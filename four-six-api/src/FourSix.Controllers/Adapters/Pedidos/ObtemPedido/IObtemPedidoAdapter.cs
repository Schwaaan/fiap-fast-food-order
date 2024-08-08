namespace FourSix.Controllers.Adapters.Pedidos.ObtemPedido
{
    public interface IObtemPedidoAdapter
    {
        Task<ObtemPedidoResponse> Obter(Guid id);
    }
}
