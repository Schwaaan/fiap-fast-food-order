namespace FourSix.Controllers.Adapters.Pedidos.CancelaPedido
{
    public interface ICancelaPedidoAdapter
    {
        Task<CancelaPedidoResponse> Cancelar(CancelaPedidoRequest pedido);
    }
}
