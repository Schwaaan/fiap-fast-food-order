namespace FourSix.Domain.Entities.PedidoAggregate
{
    public class PedidoCheckout
    {
        public PedidoCheckout()
        {
        }

        public PedidoCheckout(Guid pedidoId, int sequencia, EnumStatusPedido statusId, DateTime dataStatus)
        {
            PedidoId = pedidoId;
            Sequencia = sequencia;
            StatusId = statusId;
            DataStatus = dataStatus;
        }

        public Guid PedidoId { get; }
        public int Sequencia { get; }
        public EnumStatusPedido StatusId { get; }
        public DateTime DataStatus { get; }

        public StatusPedido Status { get; set; }
    }
}
