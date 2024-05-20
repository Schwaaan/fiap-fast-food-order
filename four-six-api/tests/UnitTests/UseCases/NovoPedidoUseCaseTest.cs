using FourSix.Controllers.Adapters.Pedidos.NovoPedido;
using FourSix.Controllers.ViewModels;
using FourSix.Domain.Entities.PedidoAggregate;
using FourSix.Domain.Requests;
using FourSix.UseCases.Interfaces;
using FourSix.UseCases.UseCases.Pedidos.NovoPedido;
using Moq;
using System.Linq.Expressions;

namespace UnitTests.UseCases
{
    public class NovoPedidoUseCaseTest
    {
        [Fact]
        public async Task Deve_Inserir_NovoPedido()
        {
            // Arrange
            var mockRepository = new Mock<IPedidoRepository>();
            var mockCheckoutRepository = new Mock<IPedidoCheckoutRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockPagamentoService = new Mock<IPagamentoService>();

            mockRepository.Setup(repo => repo.Incluir(It.IsAny<Pedido>())).Returns(Task.CompletedTask);
            NovoPedidoUseCase useCase = new NovoPedidoUseCase(mockRepository.Object, mockPagamentoService.Object, mockUnitOfWork.Object);

            var pedido = GerarPedido();

            mockPagamentoService
                .Setup(x => x.GerarPagamento(It.IsAny<Guid>(), It.IsAny<decimal>()))
                .ReturnsAsync(new PagamentoResponse { CodigoQR="xpto", Id=Guid.NewGuid(), PedidoId=pedido.Id, ValorPago=pedido.ValorTotal, ValorPedido=pedido.ValorTotal });

            // Act
            var pedidoIncluido = await useCase.Execute(pedido.DataPedido, pedido.ClienteId, pedido.Itens.Select(i => new Tuple<Guid, decimal, int, string>(i.ProdutoId, i.ValorUnitario, i.Quantidade, i.Observacao)).ToList());

            // Assert
            Assert.IsType<Pedido>(pedidoIncluido);
            mockRepository.Verify(repo => repo.Incluir(It.IsAny<Pedido>()), Times.Once);
            mockUnitOfWork.Verify(unit => unit.Save(), Times.Once);
            mockPagamentoService.Verify(unit => unit.GerarPagamento(It.IsAny<Guid>(), It.IsAny<decimal>()), Times.Once);
        }

        [Fact]
        public async void Deve_RetornarErro_Inserir_PedidoExistente()
        {
            //Arrange
            var mockRepository = new Mock<IPedidoRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockPagamentoService = new Mock<IPagamentoService>();
            var pedido = GerarPedido();

            mockRepository.Setup(repo => repo.Incluir(It.IsAny<Pedido>())).Returns(Task.CompletedTask);
            mockRepository.Setup(repo => repo.Listar(It.IsAny<Expression<Func<Pedido, bool>>>())).Returns((Expression<Func<Pedido, bool>> predicate) => new List<Pedido> { pedido }.AsQueryable().Where(predicate).ToList());
            NovoPedidoUseCase useCase = new NovoPedidoUseCase(mockRepository.Object, mockPagamentoService.Object, mockUnitOfWork.Object);
            

            //Act
            Func<Task> act = () => useCase.Execute(pedido.DataPedido, pedido.ClienteId, pedido.Itens.Select(i => new Tuple<Guid, decimal, int, string>(i.ProdutoId, i.ValorUnitario, i.Quantidade, i.Observacao)).ToList());

            //& Assert
            await Assert.ThrowsAsync<Exception>(act);
            mockRepository.Verify(repo => repo.Incluir(It.IsAny<Pedido>()), Times.Never);
            mockUnitOfWork.Verify(unit => unit.Save(), Times.Never);
        }

        private Pedido GerarPedido()
        {
            Random random = new Random();
            var quantidadeMinutos = random.Next(100, 200);
            var dataPedido = DateTime.Now.AddDays(-quantidadeMinutos);


            var guidPedido = Guid.NewGuid();
            var itens = new List<PedidoItem>();
            var quantidadeItens = random.Next(1, 4);
            for (int j = 0; j < quantidadeItens; j++)
            {
                var guidProduto = Guid.NewGuid();
                var valorItem = (decimal)(random.NextDouble() * (25.0 - 5.0) + 5.0);
                itens.Add(new PedidoItem(guidPedido, guidProduto, valorItem, 1, null));
            }

            var checkouts = new List<PedidoCheckout>
            {
                new PedidoCheckout(guidPedido, 0, EnumStatusPedido.Criado, dataPedido)
            };

            return new Pedido(guidPedido, dataPedido, Guid.NewGuid(), itens, checkouts);
        }
    }
}