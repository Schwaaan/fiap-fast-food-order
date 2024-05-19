using FourSix.Controllers.Adapters.Pedidos.AlteraStatusPedido;
using FourSix.Controllers.Adapters.Pedidos.CancelaPedido;
using FourSix.Controllers.Adapters.Pedidos.NovoPedido;
using FourSix.Controllers.ViewModels;
using FourSix.Domain.Entities.PedidoAggregate;
using FourSix.UseCases.UseCases.Pedidos.AlteraStatusPedido;
using FourSix.UseCases.UseCases.Pedidos.CancelaPedido;
using Moq;

namespace UnitTests.Adapter
{
    public class CancelaPedidoAdapterTest
    {
        [Fact]
        public async Task Deve_CancelarPedido()
        {
            // Arrange
            var mockUseCase = new Mock<ICancelaPedidoUseCase>();

            var pedido = GerarPedido();

            mockUseCase
                .Setup(x => x.Execute(pedido.Id, It.IsAny<DateTime>()))
                .ReturnsAsync(pedido);

            var adapter = new CancelaPedidoAdapter(mockUseCase.Object);
            var pedidoModel = new PedidoModel(pedido);

            var pedidoRequest = new CancelaPedidoRequest
            {
                PedidoId = pedido.Id,
                DataCancelamento=DateTime.Now
            };

            // Act
            var response = await adapter.Cancelar(pedidoRequest);

            // Assert
            Assert.NotNull(response);
            Assert.IsType<CancelaPedidoResponse>(response);
            Assert.Equal(pedidoModel.Id, response.Pedido.Id);
   
            mockUseCase.Verify(x => x.Execute(pedidoRequest.PedidoId, pedidoRequest.DataCancelamento));
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