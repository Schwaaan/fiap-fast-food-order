using FourSix.Domain.Entities.PedidoAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Domain
{
    public class PedidoItemTest
    {
        [Fact]
        public void Deve_Criar_Classe_PedidoItem()
        {
            var id = Guid.NewGuid();
            var dataPedido = DateTime.Now;
            var produtoId = Guid.NewGuid();

            PedidoItem item = new PedidoItem(id, produtoId, 10.0m, 1, "Sem Alface");

            item.AdicionarQuantidade(5);

            Assert.Equal(id, item.PedidoId);
            Assert.Equal(produtoId, item.ProdutoId);
            Assert.Equal(10.0m, item.ValorUnitario);
            Assert.Equal(6, item.Quantidade);
            Assert.Equal("Sem Alface", item.Observacao);
        }
    }
}
