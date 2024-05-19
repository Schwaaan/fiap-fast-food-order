using FourSix.Domain.Entities.PedidoAggregate;

namespace FourSix.UseCases.UseCases.Pedidos.ObtemPedidosPorStatus
{
    public interface IObtemPedidosPorStatusUseCase
    {
        Task<ICollection<Pedido>> Execute(EnumStatusPedido statusId);
    }
}
