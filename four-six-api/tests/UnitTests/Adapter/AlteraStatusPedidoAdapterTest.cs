using FourSix.Controllers.Adapters.Pedidos.AlteraStatusPedido;
using FourSix.Controllers.Adapters.Pedidos.NovoPedido;
using FourSix.Controllers.ViewModels;
using FourSix.Domain.Entities.PedidoAggregate;
using FourSix.UseCases.UseCases.Pedidos.AlteraStatusPedido;
using FourSix.UseCases.UseCases.Pedidos.NovoPedido;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Adapter
{
    public class AlteraStatusPedidoAdapterTest
    {
        [Fact]
        public async Task Deve_AlterarStatusPedido_ParaAguardandoPagamento()
        {
            // Arrange
            var mockUseCase = new Mock<IAlteraStatusPedidoUseCase>();

            var pedido = GerarPedido();

            mockUseCase
                .Setup(x => x.Execute(pedido.Id, EnumStatusPedido.AguardandoPagamento, It.IsAny<DateTime>()))
                .ReturnsAsync(pedido);

            var adapter = new AlteraStatusPedidoAdapter(mockUseCase.Object);
            var pedidoModel = new PedidoModel(pedido);

            // Act
            var response = await adapter.Alterar(pedido.Id, EnumStatusPedido.AguardandoPagamento);

            // Assert
            Assert.NotNull(response);
            Assert.IsType<AlteraStatusPedidoResponse>(response);
            Assert.Equal(pedidoModel.Id, response.Pedido.Id);
            Assert.Equal(pedidoModel.StatusId, response.Pedido.StatusId);

            mockUseCase.Verify(x => x.Execute(pedido.Id, EnumStatusPedido.AguardandoPagamento, It.IsAny<DateTime>()), Times.Once);
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
