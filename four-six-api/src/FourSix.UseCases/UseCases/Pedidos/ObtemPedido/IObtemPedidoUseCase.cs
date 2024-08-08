using FourSix.Domain.Entities.PedidoAggregate;

namespace FourSix.UseCases.UseCases.Pedidos.ObtemPedido
{
    public interface IObtemPedidoUseCase
    {
        Task<Pedido> Execute(Guid id);
    }
}
