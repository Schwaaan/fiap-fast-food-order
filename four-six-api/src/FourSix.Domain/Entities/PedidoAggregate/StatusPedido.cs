namespace FourSix.Domain.Entities.PedidoAggregate
{
    public enum EnumStatusPedido
    {
        Criado = 1,
        AguardandoPagamento = 2,
        Pago = 3,
        EmPreparacao = 4,
        Pronto = 5,
        Finalizado = 6,
        Cancelado = 7
    }

    public class StatusPedido
    {
        public EnumStatusPedido Id { get; }
        public string Descricao { get; }
    }
}
