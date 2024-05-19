using FourSix.Domain.Entities.PedidoAggregate;

namespace FourSix.UseCases.UseCases.Pedidos.CancelaPedido
{
    public interface ICancelaPedidoUseCase
    {
        /// <summary>
        /// Executa o Caso de Uso
        /// </summary>
        Task<Pedido> Execute(Guid pedidoId, DateTime dataCancelamento);
    }
}
