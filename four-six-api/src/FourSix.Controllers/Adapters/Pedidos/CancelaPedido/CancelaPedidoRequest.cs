using Swashbuckle.AspNetCore.Annotations;

namespace FourSix.Controllers.Adapters.Pedidos.CancelaPedido
{
    [SwaggerSchema(Required = new[] { "numeroPedido", "dataPedido", "clienteId", "items" })]
    public class CancelaPedidoRequest
    {
        /// <summary>
        /// Id do Pedido
        /// </summary>
        /// <example>78e3b8d0-be9a-4407-9304-c61788797808</example>
        [SwaggerSchema(Nullable = false)]
        public Guid PedidoId { get; set; }

        /// <summary>
        /// Data do Cancelamento
        /// </summary>
        /// <example>2023-10-24T18:00</example>
        [SwaggerSchema(Nullable = false)]
        public DateTime DataCancelamento { get; set; }
    }
}
