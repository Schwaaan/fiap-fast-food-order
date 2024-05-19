using FourSix.Domain.Entities.PedidoAggregate;

namespace FourSix.UseCases.Interfaces
{
    public interface IPedidoCheckoutRepository : IGetRepository<PedidoCheckout, EnumStatusPedido>, ISetRepository<PedidoCheckout, EnumStatusPedido>
    {
    }
}
