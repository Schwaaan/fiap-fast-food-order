using FourSix.Domain.Entities.PedidoAggregate;

namespace FourSix.Controllers.Adapters.Pedidos.AlteraStatusPedido
{
    public interface IAlteraStatusPedidoAdapter
    {
        Task<AlteraStatusPedidoResponse> Alterar(Guid pedidoId, EnumStatusPedido statusId);
    }
}
