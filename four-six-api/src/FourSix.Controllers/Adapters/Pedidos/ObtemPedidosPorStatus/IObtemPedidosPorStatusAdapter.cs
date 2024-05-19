using FourSix.Domain.Entities.PedidoAggregate;
using FourSix.WebApi.UseCases.Pedidos.ObtemPedido;

namespace FourSix.Controllers.Adapters.Pedidos.ObtemPedidosPorStatus
{
    public interface IObtemPedidosPorStatusAdapter
    {
        Task<ObtemPedidosPorStatusResponse> Listar(EnumStatusPedido statusId);
    }
}
