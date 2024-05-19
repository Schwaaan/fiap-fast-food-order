using FourSix.Domain.Entities.PedidoAggregate;
using FourSix.UseCases.Interfaces;
using FourSix.UseCases.UseCases.Pedidos.ObtemPedidos;
using Moq;
using System.Linq.Expressions;

namespace UnitTests.UseCases
{
    public class ObtemPedidosUseCaseTest
    {
        [Fact]
        public async void Deve_Listar_Pedidos_Nao_Finalizados()
        {
            //Arrange
            var mockRepository = new Mock<IPedidoRepository>();
            var mockCheckoutRepository = new Mock<IPedidoCheckoutRepository>();
            var pedidos = GerarPedidos();

            mockRepository.Setup(repo => repo.Listar(It.IsAny<Expression<Func<Pedido, bool>>>())).Returns((Expression<Func<Pedido, bool>> predicate) => pedidos.AsQueryable().Where(predicate).ToList());
            ObtemPedidosUseCase useCase = new ObtemPedidosUseCase(mockRepository.Object);


            //Act
            var pedidosNaoFinalizados = await useCase.Execute();

            //Assert
            Assert.NotNull(pedidosNaoFinalizados);
            Assert.Equal(2, pedidosNaoFinalizados.Count);
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
                if (i != 0 && i != 3)
                    quantidadeCheckout = 6;
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