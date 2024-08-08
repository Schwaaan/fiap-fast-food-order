using FourSix.Domain.Entities.PedidoAggregate;
using FourSix.UseCases.Interfaces;

namespace FourSix.UseCases.UseCases.Pedidos.ObtemPedido
{
    public class ObtemPedidoUseCase : IObtemPedidoUseCase
    {
        private readonly IPedidoRepository _pedidoRepository;

        public ObtemPedidoUseCase(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public Task<Pedido> Execute(Guid id) => ObterPedido(id);

        private async Task<Pedido> ObterPedido(Guid id)
        {
            var pedido = _pedidoRepository.Listar(x => x.Id == id).FirstOrDefault();

            return pedido;
        }
    }
}
