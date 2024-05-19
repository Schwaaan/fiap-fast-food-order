namespace FourSix.Controllers.Adapters.Pedidos.NovoPedido
{
    public interface INovoPedidoAdapter
    {
        Task<NovoPedidoResponse> Inserir(NovoPedidoRequest pedido);
    }
}
