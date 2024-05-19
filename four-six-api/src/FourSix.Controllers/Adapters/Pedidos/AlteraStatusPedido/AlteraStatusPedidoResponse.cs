using FourSix.Controllers.ViewModels;

namespace FourSix.Controllers.Adapters.Pedidos.AlteraStatusPedido
{
    public class AlteraStatusPedidoResponse
    {
        public AlteraStatusPedidoResponse(PedidoModel pedidoModel) => Pedido = pedidoModel;
        public PedidoModel Pedido { get; }
    }
}
