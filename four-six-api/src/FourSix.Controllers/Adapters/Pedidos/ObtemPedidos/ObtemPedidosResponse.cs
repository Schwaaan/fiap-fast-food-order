using FourSix.Controllers.ViewModels;

namespace FourSix.Controllers.Adapters.Pedidos.ObtemPedidos
{
    public class ObtemPedidosResponse
    {
        public ObtemPedidosResponse(ICollection<PedidoModel> pedidosModel) => Pedidos = pedidosModel;
        public ICollection<PedidoModel> Pedidos { get; }
    }
}
