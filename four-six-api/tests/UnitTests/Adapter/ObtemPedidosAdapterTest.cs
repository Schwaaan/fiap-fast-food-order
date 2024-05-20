using FourSix.Controllers.Adapters.Pedidos.ObtemPedidos;
using FourSix.Controllers.Gateways.Integrations.ProdutoIntegration;
using FourSix.Controllers.ViewModels;
using FourSix.Domain.Entities.PedidoAggregate;
using FourSix.UseCases.UseCases.Pedidos.ObtemPedidos;
using Moq;

namespace UnitTests.Adapter
{
    public class ObtemPedidosAdapterTest
    {
        [Fact]
        public async Task Listar_Deve_RetornarTodosPedidosResponse()
        {
            // Arrange
            var mockUseCase = new Mock<IObtemPedidosUseCase>();
            var mockProdutoService = new Mock<IProdutoService>();

            var pedidos = GerarPedidos(5);
            var produto = new ProdutoRequest
            {
                Id = Guid.NewGuid(),
                Descricao = "Produto teste"
            };

           mockUseCase
                .Setup(x => x.Execute())
                .ReturnsAsync(pedidos);

            mockProdutoService
                .Setup(x => x.GetProduto(It.IsAny<Guid>()))
                .ReturnsAsync(produto);

            var adapter = new ObtemPedidosAdapter(mockUseCase.Object, mockProdutoService.Object);
            var pedidoModel = pedidos.Select(s => new PedidoModel(s)).ToList();

            // Act
            var response = await adapter.Listar();

            // Assert
            Assert.NotNull(response);
            Assert.IsType<ObtemPedidosResponse>(response);
            Assert.Equal(5, response.Pedidos.Count);


            mockUseCase.Verify(x => x.Execute(), Times.Once);
        }

        private List<Pedido> GerarPedidos(int quantidade)
        {
            var pedidos = new List<Pedido>();
            Random random = new Random();
            var quantidadeMinutos = random.Next(100, 200);
            var dataPedido = DateTime.Now.AddDays(-quantidadeMinutos);
            if (quantidade <= 0) quantidade = 1;

            for (int i = 0; i < quantidade; i++)
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
                var quantidadeCheckout = random.Next(1, 6);
                for (int k = 0; k < quantidadeCheckout; k++)
                {
                    if (k > 0)
                    {
                        var minutosStatus = random.Next(5, 10);
                        dataStatus = dataStatus.AddMinutes(minutosStatus);
                    }
                    checkouts.Add(new PedidoCheckout(guidPedido, k, (EnumStatusPedido)k + 1, dataStatus));
                }

                pedidos.Add(new Pedido(guidPedido, dataPedido, Guid.NewGuid(), itens, checkouts));
            }

            return pedidos;
        }
    }
}
