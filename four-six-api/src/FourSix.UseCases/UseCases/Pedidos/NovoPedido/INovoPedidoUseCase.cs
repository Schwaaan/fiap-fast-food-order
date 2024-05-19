using FourSix.Domain.Entities.PedidoAggregate;

namespace FourSix.UseCases.UseCases.Pedidos.NovoPedido
{
    public interface INovoPedidoUseCase
    {
        /// <summary>
        /// Executa o Caso de Uso
        /// </summary>
        Task<Pedido> Execute(DateTime dataPedido, Guid? clienteId, ICollection<Tuple<Guid, decimal, int, string>> itens);
    }
}
