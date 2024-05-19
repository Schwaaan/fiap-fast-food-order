using FourSix.Domain.Entities.PedidoAggregate;
using FourSix.UseCases.Interfaces;

namespace FourSix.UseCases.UseCases.Pedidos.ObtemPedidosPorStatus
{
    public class ObtemPedidosPorStatusUseCase : IObtemPedidosPorStatusUseCase
    {
        private readonly IPedidoRepository _pedidoRepository;

        public ObtemPedidosPorStatusUseCase(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public Task<ICollection<Pedido>> Execute(EnumStatusPedido statusId) => ListarPedidos(statusId);

        private async Task<ICollection<Pedido>> ListarPedidos(EnumStatusPedido statusId)
        {
            var pedidos = _pedidoRepository.Listar(q => q.StatusId == statusId).ToList();

            return pedidos;
        }
    }
}