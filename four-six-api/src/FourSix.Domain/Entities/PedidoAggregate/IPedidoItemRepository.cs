namespace FourSix.Domain.Entities.PedidoAggregate
{
    public interface IPedidoItemRepository : IGetRepository<PedidoItem, Guid>, ISetRepository<PedidoItem>
    {
    }
}
