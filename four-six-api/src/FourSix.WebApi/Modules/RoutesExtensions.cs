using FourSix.Controllers.Adapters.Pedidos.AlteraStatusPedido;
using FourSix.Controllers.Adapters.Pedidos.CancelaPedido;
using FourSix.Controllers.Adapters.Pedidos.NovoPedido;
using FourSix.Controllers.Adapters.Pedidos.ObtemPedidos;
using FourSix.Controllers.Adapters.Pedidos.ObtemPedidosPorStatus;
using FourSix.Domain.Entities.PedidoAggregate;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Diagnostics.CodeAnalysis;

namespace FourSix.WebApi.Modules
{
    [ExcludeFromCodeCoverage]
    public static class RoutesExtensions
    {
        public static void AddRoutesMaps(this IEndpointRouteBuilder app)
        {
            #region [ Pedidos ]

            app.MapGet("pedidos",
            [SwaggerOperation(Summary = "Obtém lista de pedido")]
            (IObtemPedidosAdapter adapter) =>
            {
                return adapter.Listar();
            }).WithTags("Pedidos").AllowAnonymous();

            app.MapGet("pedidos/{statusId}",
            [SwaggerOperation(Summary = "Obtém lista de pedido por status")]
            ([SwaggerParameter("Status do pedido<br><br>Recebido = 1<br>Pago = 2<br>EmPreparacao = 3<br>Montagem = 4<br>Pronto = 5<br>Finalizado = 6<br>Cancelado = 7")] EnumStatusPedido statusId, IObtemPedidosPorStatusAdapter adapter) =>
            {
                return adapter.Listar(statusId);
            }).WithTags("Pedidos");

            app.MapPost("pedidos/anonymous",
            [SwaggerOperation(Summary = "Cria novo pedido")]
            ([FromBody] NovoPedidoRequest request, INovoPedidoAdapter adapter) =>
            {
                return adapter.Inserir(request);
            }).WithTags("Pedidos").AllowAnonymous();

            app.MapPost("pedidos",
            [SwaggerOperation(Summary = "Cria novo pedido")]
            ([FromBody] NovoPedidoRequest request, INovoPedidoAdapter adapter) =>
            {
                return adapter.Inserir(request);
            }).WithTags("Pedidos").RequireAuthorization();

            app.MapPut("pedidos/cancelamentos/anonymous",
            [SwaggerOperation(Summary = "Cancela pedido")]
            ([FromBody] CancelaPedidoRequest request, ICancelaPedidoAdapter adapter) =>
            {
                return adapter.Cancelar(request);
            }).WithTags("Pedidos").AllowAnonymous();

            app.MapPut("pedidos/{pedidoId:Guid}/status/{statusId}",
            [SwaggerOperation(Summary = "Altera status do pedido")]
            ([SwaggerParameter("ID do Pedido")] Guid pedidoId, [SwaggerParameter("Status do pedido<br><br>Recebido = 1<br>Pago = 2<br>EmPreparacao = 3<br>Montagem = 4<br>Pronto = 5<br>Finalizado = 6<br>Cancelado = 7")] EnumStatusPedido statusId, IAlteraStatusPedidoAdapter adapter) =>
            {
                return adapter.Alterar(pedidoId, statusId);
            }).WithTags("Pedidos").AllowAnonymous(); ;


            #endregion
        }
    }
}
