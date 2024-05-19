namespace FourSix.Domain.Entities.PedidoAggregate
{
    public interface IPedidoCheckoutRepository : IGetRepository<PedidoCheckout, EnumStatusPedido>, ISetRepository<PedidoCheckout, EnumStatusPedido>
    {
    }
}
