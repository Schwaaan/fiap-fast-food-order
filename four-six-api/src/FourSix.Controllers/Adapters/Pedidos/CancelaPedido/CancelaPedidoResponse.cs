using FourSix.Controllers.ViewModels;

namespace FourSix.Controllers.Adapters.Pedidos.CancelaPedido
{
    public class CancelaPedidoResponse
    {
        public CancelaPedidoResponse(PedidoModel pedidoModel) => Pedido = pedidoModel;
        public PedidoModel Pedido { get; }
    }
}
