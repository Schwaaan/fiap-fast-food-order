using FourSix.Domain.Entities.PedidoAggregate;
using FourSix.UseCases.Interfaces;

namespace FourSix.UseCases.UseCases.Pedidos.NovoPedido
{
    public class NovoPedidoUseCase : INovoPedidoUseCase
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public NovoPedidoUseCase(
            IPedidoRepository pedidoRepository,
            IUnitOfWork unitOfWork)
        {
            _pedidoRepository = pedidoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Pedido> Execute(DateTime dataPedido, Guid? clienteId, ICollection<Tuple<Guid, decimal, int, string>> itens)
        {
            var id = Guid.NewGuid();

            var pedido = await InserirPedido(
                new Pedido(id,
                dataPedido,
                clienteId,
                itens.Select(i => new PedidoItem(id, i.Item1, i.Item2, i.Item3, i.Item4)).ToList(),
                new List<PedidoCheckout> { new PedidoCheckout(id, 1, EnumStatusPedido.Criado, DateTime.Now) }));

            return pedido;
        }

        private async Task<Pedido> InserirPedido(Pedido pedido)
        {
            if (_pedidoRepository
                .Listar(q => q.NumeroPedido == pedido.NumeroPedido).Any())
            {
                throw new Exception("Pedido já existe");
            }

            await _pedidoRepository
                 .Incluir(pedido)
                 .ConfigureAwait(false);

            await _unitOfWork
                .Save()
                .ConfigureAwait(false);

            return pedido;
        }
    }
}
