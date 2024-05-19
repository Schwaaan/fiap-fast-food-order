using FourSix.Domain.Entities.PedidoAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Domain
{
    public class PedidoTest
    {
        [Fact]
        public void Deve_Criar_Classe_Pedido()
        {
            var id = Guid.NewGuid();
            var dataPedido = DateTime.Now;
            var clienteId = Guid.NewGuid();

            Pedido produto = new Pedido(id, dataPedido, clienteId, new List<PedidoItem>(), new List<PedidoCheckout>());

            produto.AdicionarItem(Guid.NewGuid(), 10.00m, 1, "Sem Alface");
            produto.AdicionarItem(Guid.NewGuid(), 15.00m, 1, "Com Bacon");

            Assert.Equal(id, produto.Id);
            Assert.Equal(dataPedido, produto.DataPedido);
            Assert.Equal(clienteId, produto.ClienteId);
            Assert.Equal(2, produto.Itens.Count);
            Assert.Equal(2, produto.TotalItens);
            Assert.Equal(25.00m, produto.ValorTotal);
            Assert.Equal(EnumStatusPedido.Criado, produto.StatusId);
        }

        [Fact]
        public void Deve_Somar_Quantidade_ItemExistente()
        {
            var id = Guid.NewGuid();
            var dataPedido = DateTime.Now;
            var clienteId = Guid.NewGuid();

            Pedido produto = new Pedido(id, dataPedido, clienteId, new List<PedidoItem>(), new List<PedidoCheckout>());

            var produtoId = Guid.NewGuid();

            produto.AdicionarItem(produtoId, 10.00m, 1, "Sem Alface");
            produto.AdicionarItem(produtoId, 15.00m, 1, "Com Bacon");

            Assert.Equal(id, produto.Id);
            Assert.Equal(dataPedido, produto.DataPedido);
            Assert.Equal(clienteId, produto.ClienteId);
            Assert.Equal(2, produto.TotalItens);
            Assert.Equal(20.00m, produto.ValorTotal);
            Assert.Equal(1, produto.Itens.Count);
            Assert.Equal(2, produto.Itens.First().Quantidade);
        }
    }
}
