using FourSix.Controllers.Adapters.Pedidos.ObtemPedidosPorStatus;
using FourSix.Controllers.ViewModels;
using FourSix.Domain.Entities.PedidoAggregate;
using FourSix.UseCases.UseCases.Pedidos.ObtemPedidosPorStatus;
using FourSix.WebApi.UseCases.Pedidos.ObtemPedido;
using Moq;

namespace UnitTests.Adapter
{
    public class ObtemPedidosPorStatusAdapterTest
    {
        [Fact]
        public async Task Listar_Pago_Deve_RetornarTodosPedidosPagoResponse()
        {
            // Arrange
            var mockUseCase = new Mock<IObtemPedidosPorStatusUseCase>();

            var pedidos = GerarPedidos();

            mockUseCase
                .Setup(x => x.Execute(EnumStatusPedido.Pago))
                .ReturnsAsync(pedidos.Where(q=>q.StatusId==EnumStatusPedido.Pago).ToList());

            var adapter = new ObtemPedidosPorStatusAdapter(mockUseCase.Object);
            var pedidoModel = pedidos.Where(q=>q.StatusId==EnumStatusPedido.Pago)
                .Select(s => new PedidoModel(s)).ToList();

            // Act
            var response = await adapter.Listar(EnumStatusPedido.Pago);

            // Assert
            Assert.NotNull(response);
            Assert.IsType<ObtemPedidosPorStatusResponse>(response);
            Assert.Equal(2, response.Pedidos.Count);


            mockUseCase.Verify(x => x.Execute(EnumStatusPedido.Pago), Times.Once);
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
