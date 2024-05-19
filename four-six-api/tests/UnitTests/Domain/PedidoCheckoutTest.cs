using FourSix.Domain.Entities.PedidoAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Domain
{
    public class PedidoCheckoutTest
    {
        [Fact]
        public void Deve_Criar_Classe_PedidoCheckout()
        {
            var id = Guid.NewGuid();
            var dataPedido = DateTime.Now;
            var clienteId = Guid.NewGuid();

            PedidoCheckout checkout = new PedidoCheckout(id,0,EnumStatusPedido.Criado,dataPedido);

            Assert.Equal(id, checkout.PedidoId);
            Assert.Equal(dataPedido, checkout.DataStatus);
            Assert.Equal(0, checkout.Sequencia);
            Assert.Equal(EnumStatusPedido.Criado, checkout.StatusId);
        }
    }
}
