using FourSix.Domain.Entities.PedidoAggregate;
using FourSix.UseCases.Interfaces;
using FourSix.UseCases.UseCases.Pedidos.AlteraStatusPedido;
using FourSix.UseCases.UseCases.Pedidos.NovoPedido;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.UseCases
{
    public class AlteraStatusPedidoUseCaseTest
    {
        [Fact]
        public async void Deve_Alterar_StatusPedido_ParaAguardando()
        {
            // Arrange
            var mockRepository = new Mock<IPedidoRepository>();
            var mockCheckoutRepository = new Mock<IPedidoCheckoutRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockProducaoService = new Mock<IProducaoService>();

            var pedidos = GerarPedidos();
            var pedidoAlterar = pedidos.Where(q=>q.StatusId==EnumStatusPedido.Criado).First();

            mockRepository.Setup(repo => repo.Alterar(It.IsAny<Pedido>())).Returns(Task.CompletedTask);
            mockRepository.Setup(repo => repo.Listar(It.IsAny<Expression<Func<Pedido, bool>>>())).Returns((Expression<Func<Pedido, bool>> predicate) => pedidos.AsQueryable().Where(predicate).ToList());
            mockCheckoutRepository.Setup(repo => repo.Listar(It.IsAny<Expression<Func<PedidoCheckout, bool>>>())).Returns((Expression<Func<PedidoCheckout, bool>> predicate) => pedidoAlterar.HistoricoCheckout.AsQueryable().Where(predicate).ToList());
            mockCheckoutRepository.Setup(repo => repo.Incluir(It.IsAny<PedidoCheckout>())).Returns(Task.CompletedTask);
            AlteraStatusPedidoUseCase useCase = new AlteraStatusPedidoUseCase(mockRepository.Object, mockProducaoService.Object, mockUnitOfWork.Object, mockCheckoutRepository.Object);


            // Act
            var pedidoAlterado = await useCase.Execute(pedidoAlterar.Id, EnumStatusPedido.AguardandoPagamento,DateTime.Now);

            // Assert
            Assert.IsType<Pedido>(pedidoAlterado);
            mockRepository.Verify(repo => repo.Alterar(It.IsAny<Pedido>()), Times.Once);
            mockRepository.Verify(repo => repo.Listar(It.IsAny<Expression<Func<Pedido, bool>>>()), Times.Exactly(2));
            mockCheckoutRepository.Verify(repo => repo.Listar(It.IsAny<Expression<Func<PedidoCheckout, bool>>>()), Times.Once);
            mockCheckoutRepository.Verify(repo => repo.Incluir(It.IsAny<PedidoCheckout>()), Times.Once);
            mockUnitOfWork.Verify(unit => unit.Save(), Times.Once);
        }

        [Fact]
        public async void Deve_Retornar_Exception_Pedido_Nao_Encontrado()
        {
            // Arrange
            var mockRepository = new Mock<IPedidoRepository>();
            var mockCheckoutRepository = new Mock<IPedidoCheckoutRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockProducaoService = new Mock<IProducaoService>();

            var pedidos = GerarPedidos();
            var pedidoAlterar = pedidos.Where(q => q.StatusId == EnumStatusPedido.Criado).First();

            mockRepository.Setup(repo => repo.Alterar(It.IsAny<Pedido>())).Returns(Task.CompletedTask);
            mockRepository.Setup(repo => repo.Listar(It.IsAny<Expression<Func<Pedido, bool>>>())).Returns((Expression<Func<Pedido, bool>> predicate) => pedidos.AsQueryable().Where(predicate).ToList());
            mockCheckoutRepository.Setup(repo => repo.Listar(It.IsAny<Expression<Func<PedidoCheckout, bool>>>())).Returns((Expression<Func<PedidoCheckout, bool>> predicate) => pedidoAlterar.HistoricoCheckout.AsQueryable().Where(predicate).ToList());
            mockCheckoutRepository.Setup(repo => repo.Incluir(It.IsAny<PedidoCheckout>())).Returns(Task.CompletedTask);
            AlteraStatusPedidoUseCase useCase = new AlteraStatusPedidoUseCase(mockRepository.Object, mockProducaoService.Object, mockUnitOfWork.Object, mockCheckoutRepository.Object);


            // Act
            Func<Task> act = () => useCase.Execute(Guid.NewGuid(), EnumStatusPedido.AguardandoPagamento, DateTime.Now);

            //Assert
            await Assert.ThrowsAsync<Exception>(act);
            mockRepository.Verify(repo => repo.Incluir(It.IsAny<Pedido>()), Times.Never);
            mockUnitOfWork.Verify(unit => unit.Save(), Times.Never);
        }

        private List<Pedido> GerarPedidos()
        {
            var pedidos = new List<Pedido>();
            Random random = new Random();
            var quantidadeMinutos = random.Next(100, 200);
            var dataPedido = DateTime.Now.AddDays(-quantidadeMinutos);

            for (int i = 0; i < 5; i++)
            {
                var guidPedido = Guid.NewGuid();
                var itens = new List<PedidoItem>();
                var quantidadeItens = random.Next(1, 4);
                for (int j = 0; j < quantidadeItens; j++)
                {
                    var guidProduto = Guid.NewGuid();
                    var valorItem = (decimal)(random.NextDouble() * (25.0 - 5.0) + 5.0);
                    itens.Add(new PedidoItem(guidPedido, guidProduto, valorItem, 1, null));
                }

                var dataStatus = dataPedido;
                var checkouts = new List<PedidoCheckout>();
                var quantidadeCheckout = 3;
                if (i != 0)
                    quantidadeCheckout = 1;
                EnumStatusPedido ultimoStatus = EnumStatusPedido.Criado;
                for (int k = 0; k < quantidadeCheckout; k++)
                {
                    if (k > 0)
                    {
                        var minutosStatus = random.Next(5, 10);
                        dataStatus = dataStatus.AddMinutes(minutosStatus);
                    }
                    ultimoStatus = (EnumStatusPedido)k + 1;
                    checkouts.Add(new PedidoCheckout(guidPedido, k, ultimoStatus, dataStatus));
                }

                var pedido = new Pedido(guidPedido, dataPedido, Guid.NewGuid(), itens, checkouts);
                pedido.AlterarStatus(ultimoStatus);
                pedidos.Add(pedido);
            }

            return pedidos;
        }
    }
}
