using FourSix.Domain.Entities.PedidoAggregate;

namespace FourSix.UseCases.UseCases.Pedidos.ObtemPedidos

{
    public interface IObtemPedidosUseCase
    {
        Task<ICollection<Pedido>> Execute();

    }
}
