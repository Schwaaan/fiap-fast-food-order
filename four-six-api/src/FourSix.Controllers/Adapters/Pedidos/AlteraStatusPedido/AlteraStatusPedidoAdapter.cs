using FourSix.Controllers.ViewModels;
using FourSix.Domain.Entities.PedidoAggregate;
using FourSix.UseCases.UseCases.Pedidos.AlteraStatusPedido;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FourSix.Controllers.Adapters.Pedidos.AlteraStatusPedido
{
    public class AlteraStatusPedidoAdapter : IAlteraStatusPedidoAdapter
    {
        private readonly IAlteraStatusPedidoUseCase _useCase;

        public AlteraStatusPedidoAdapter(IAlteraStatusPedidoUseCase useCase)
        {
            _useCase = useCase;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlteraStatusPedidoResponse))]
        public async Task<AlteraStatusPedidoResponse> Alterar(Guid pedidoId, EnumStatusPedido statusId)
        {
            var model = new PedidoModel(await _useCase.Execute(pedidoId, statusId, DateTime.Now));

            return new AlteraStatusPedidoResponse(model);
        }
    }
}
