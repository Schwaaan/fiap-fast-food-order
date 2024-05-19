using FourSix.Domain.Entities.PedidoAggregate;

namespace FourSix.UseCases.Interfaces
{
    public interface IPedidoRepository : IGetRepository<Pedido>, ISetRepository<Pedido>
    {
    }
}
