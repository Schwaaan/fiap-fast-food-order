using FourSix.Controllers.ViewModels;

namespace FourSix.Controllers.Adapters.Pedidos.ObtemPedido
{
    public class ObtemPedidoResponse
    {
        public ObtemPedidoResponse(PedidoModel pedidoModel) => Pedido = pedidoModel;
        public PedidoModel Pedido { get; }
    }
}
