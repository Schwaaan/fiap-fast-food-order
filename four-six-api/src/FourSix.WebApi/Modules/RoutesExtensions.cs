using FourSix.Controllers.Adapters.Clientes.NovoCliente;
using FourSix.Controllers.Adapters.Clientes.ObtemCliente;
using FourSix.Controllers.Adapters.Clientes.SolicitaLgpd;
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
            #region [ Clientes ]

            app.MapGet("clientes/{cpf}",
            [SwaggerOperation(Summary = "Obtém cliente")]
            ([SwaggerParameter("CPF do cliente")] string cpf, IObtemClienteAdapter adapter) =>
            {
                return adapter.Obter(cpf);
            }).WithTags("Clientes");

            app.MapPost("clientes",
            [SwaggerOperation(Summary = "Cria novo cliente")]
            ([FromBody] NovoClienteRequest request, INovoClienteAdapter adapter) =>
            {
                return adapter.Inserir(request);
            }).WithTags("Clientes");

            app.MapPost("clientes/lgpd",
           [SwaggerOperation(Summary = "Solicita expurgo de dados Lgpd")]
            ([FromBody] SolicitaLgpdRequest request, ISolicitaLgpdAdapter adapter) =>
           {
               return adapter.Inserir(request);
           }).WithTags("Clientes");

            #endregion

            #region [ Pedidos ]

            app.MapGet("pedidos",
            [SwaggerOperation(Summary = "Obtém lista de pedido")]
            (IObtemPedidosAdapter adapter) =>
            {
                return adapter.Listar();
            }).WithTags("Pedidos").AllowAnonymous();

            app.MapGet("pedidos/{statusId}",
            [SwaggerOperation(Summary = "Obtém lista de pedido por status")]
            ([SwaggerParameter("Status do pedido<br> < br > Criado = 1 < br > Aguardando Pagamento = 2 < br > Pago = 3 < br > EmPreparacao = 4 < br > Pronto = 5 < br > Finalizado = 6 < br > Cancelado = 7 < br > Pagamento Recusado = 8")] EnumStatusPedido statusId, IObtemPedidosPorStatusAdapter adapter) =>
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
            ([SwaggerParameter("ID do Pedido")] Guid pedidoId, [SwaggerParameter("Status do pedido<br><br>Criado = 1<br>Aguardando Pagamento = 2<br>Pago = 3<br>EmPreparacao = 4<br>Pronto = 5<br>Finalizado = 6<br>Cancelado = 7<br>Pagamento Recusado = 8")] EnumStatusPedido statusId, IAlteraStatusPedidoAdapter adapter) =>
            {
                return adapter.Alterar(pedidoId, statusId);
            }).WithTags("Pedidos").AllowAnonymous(); ;


            #endregion
        }
    }
}
