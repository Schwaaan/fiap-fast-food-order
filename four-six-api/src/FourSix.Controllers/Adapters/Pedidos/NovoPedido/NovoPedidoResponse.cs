using FourSix.Controllers.ViewModels;

namespace FourSix.Controllers.Adapters.Pedidos.NovoPedido
{
    public class NovoPedidoResponse
    {
        public NovoPedidoResponse(PedidoModel pedidoModel) => Pedido = pedidoModel;
        public PedidoModel Pedido { get; }
    }
}
