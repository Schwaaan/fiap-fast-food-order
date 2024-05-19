using FourSix.Domain.Entities.PedidoAggregate;
using FourSix.UseCases.Interfaces;

namespace FourSix.UseCases.UseCases.Pedidos.CancelaPedido
{
    public class CancelaPedidoUseCase : ICancelaPedidoUseCase
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IPedidoCheckoutRepository _pedidoStatusRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CancelaPedidoUseCase(
            IPedidoRepository pedidoRepository,
            IUnitOfWork unitOfWork,
            IPedidoCheckoutRepository pedidoStatusRepository)
        {
            _pedidoRepository = pedidoRepository;
            _unitOfWork = unitOfWork;
            _pedidoStatusRepository = pedidoStatusRepository;
        }

        public Task<Pedido> Execute(Guid pedidoId, DateTime dataCancelamento) => CancelarPedido(pedidoId, dataCancelamento);

        private async Task<Pedido> CancelarPedido(Guid pedidoId, DateTime dataCancelamento)
        {
            var pedido = _pedidoRepository.Listar(q => q.Id == pedidoId).FirstOrDefault();

            if (pedido == null)
            {
                throw new Exception("Pedido não encontrado");
            }

            var novaSequencia = _pedidoStatusRepository.Listar(l => l.PedidoId == pedidoId).Max(l => l.Sequencia) + 1;

            pedido.AlterarStatus(EnumStatusPedido.Cancelado);
            await _pedidoRepository.Alterar(pedido);

            await _pedidoStatusRepository
                 .Incluir(new PedidoCheckout(pedidoId, novaSequencia, EnumStatusPedido.Cancelado, dataCancelamento))
                 .ConfigureAwait(false);

            await _unitOfWork
                .Save()
                .ConfigureAwait(false);

            pedido = _pedidoRepository.Listar(q => q.Id == pedidoId).FirstOrDefault();

            return pedido;
        }
    }
}
