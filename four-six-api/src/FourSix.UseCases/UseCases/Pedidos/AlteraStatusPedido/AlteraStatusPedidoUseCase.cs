using FourSix.Domain.Entities.PedidoAggregate;
using FourSix.UseCases.Interfaces;

namespace FourSix.UseCases.UseCases.Pedidos.AlteraStatusPedido
{
    public class AlteraStatusPedidoUseCase : IAlteraStatusPedidoUseCase
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IPedidoCheckoutRepository _pedidoCheckoutRepository;
        private readonly IProducaoService _producaoService;
        private readonly IUnitOfWork _unitOfWork;

        public AlteraStatusPedidoUseCase(
            IPedidoRepository pedidoRepository,
            IProducaoService producaoService,
            IUnitOfWork unitOfWork,
            IPedidoCheckoutRepository pedidoStatusRepository)
        {
            _producaoService = producaoService;
            _pedidoRepository = pedidoRepository;
            _unitOfWork = unitOfWork;
            _pedidoCheckoutRepository = pedidoStatusRepository;
        }

        public Task<Pedido> Execute(Guid pedidoId, EnumStatusPedido statusId, DateTime dataStatus) => AlterarStatusPedido(pedidoId, statusId, dataStatus);

        private async Task<Pedido> AlterarStatusPedido(Guid pedidoId, EnumStatusPedido statusId, DateTime dataStatus)
        {
            var pedido = _pedidoRepository.Listar(q => q.Id == pedidoId).FirstOrDefault(); ;

            if (pedido == null)
            {
                throw new Exception("Pedido não encontrado");
            }

            var novaSequencia = _pedidoCheckoutRepository.Listar(l => l.PedidoId == pedidoId).Max(l => l.Sequencia) + 1;

            pedido.AlterarStatus(statusId);
            await _pedidoRepository.Alterar(pedido);

            await _pedidoCheckoutRepository
                 .Incluir(new PedidoCheckout(pedidoId, novaSequencia, statusId, dataStatus))
                 .ConfigureAwait(false);

            if (statusId == EnumStatusPedido.Pago)
                await _producaoService.IniciarProducao(pedidoId)
                    .ConfigureAwait(false);

            await _unitOfWork
                .Save()
                .ConfigureAwait(false);

            //Busca com os dados completos
            pedido = _pedidoRepository.Listar(q => q.Id == pedidoId).FirstOrDefault();

            return pedido;
        }
    }
}
