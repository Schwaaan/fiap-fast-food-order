using FourSix.Controllers.ViewModels;

namespace FourSix.WebApi.UseCases.Pedidos.ObtemPedido
{
    public class ObtemPedidosPorStatusResponse
    {
        public ObtemPedidosPorStatusResponse(ICollection<PedidoModel> pedidosModel) => Pedidos = pedidosModel;
        public ICollection<PedidoModel> Pedidos { get; }
    }
}
