using FourSix.Controllers.Adapters.Pedidos.NovoPedido;
using FourSix.Controllers.ViewModels;
using FourSix.Domain.Entities.PedidoAggregate;
using FourSix.UseCases.UseCases.Pedidos.NovoPedido;
using Moq;

namespace UnitTests.Adapter
{
    public class NovoPedidoAdapterTest
    {
        [Fact]
        public async Task Inserir_Deve_RetornarNovoPedidoResponse()
        {
            // Arrange
            var mockUseCase = new Mock<INovoPedidoUseCase>();
 
            var pedido = GerarPedido();
            var pedidoRequest = new NovoPedidoRequest
            {
                ClienteId = pedido.ClienteId,
                DataPedido = pedido.DataPedido,
                Items = pedido.Itens.Select(s => new NovoPedidoItemRequest
                {
                    ItemPedidoId = s.ProdutoId,
                    Quantidade = s.Quantidade,
                    ValorUnitario = s.ValorUnitario,
                    Observacao = s.Observacao
                }).ToList()
            };

            mockUseCase
                .Setup(x => x.Execute(pedidoRequest.DataPedido, pedidoRequest.ClienteId, It.IsAny<List<Tuple<Guid, decimal, int, string>>>()))
                .ReturnsAsync(pedido);

            var adapter = new NovoPedidoAdapter(mockUseCase.Object);
            var pedidoModel = new PedidoModel(pedido);

            // Act
            var response = await adapter.Inserir(pedidoRequest);

            // Assert
            Assert.NotNull(response);
            Assert.IsType<NovoPedidoResponse>(response);
            Assert.Equal(pedidoModel.Id, response.Pedido.Id);
            Assert.Equal(pedidoModel.TotalItens, response.Pedido.TotalItens);
            Assert.Equal(pedidoModel.ValorTotal, response.Pedido.ValorTotal);

            mockUseCase.Verify(x => x.Execute(pedidoRequest.DataPedido, pedidoRequest.ClienteId, It.IsAny<List<Tuple<Guid, decimal, int, string>>>()), Times.Once);
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
