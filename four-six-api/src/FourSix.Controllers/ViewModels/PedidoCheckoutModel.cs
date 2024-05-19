using FourSix.Domain.Entities.PedidoAggregate;

namespace FourSix.Controllers.ViewModels
{
    public class PedidoCheckoutModel
    {
        public PedidoCheckoutModel(PedidoCheckout pedidoCheckout)
        {
            PedidoId = pedidoCheckout.PedidoId;
            Sequencia = pedidoCheckout.Sequencia;
            StatusId = pedidoCheckout.StatusId;
            DataStatus = pedidoCheckout.DataStatus;
        }

        public Guid PedidoId { get; }
        public int Sequencia { get; }
        public EnumStatusPedido StatusId { get; }
        public DateTime DataStatus { get; }
    }
}
