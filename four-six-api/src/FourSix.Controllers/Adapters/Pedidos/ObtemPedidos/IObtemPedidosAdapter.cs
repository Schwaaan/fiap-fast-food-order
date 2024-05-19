namespace FourSix.Controllers.Adapters.Pedidos.ObtemPedidos
{
    public interface IObtemPedidosAdapter
    {
        Task<ObtemPedidosResponse> Listar();
    }
}
