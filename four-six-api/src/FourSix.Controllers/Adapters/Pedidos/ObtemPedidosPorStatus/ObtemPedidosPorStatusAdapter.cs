using FourSix.Controllers.ViewModels;
using FourSix.Domain.Entities.PedidoAggregate;
using FourSix.UseCases.UseCases.Pedidos.ObtemPedidosPorStatus;
using FourSix.WebApi.UseCases.Pedidos.ObtemPedido;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FourSix.Controllers.Adapters.Pedidos.ObtemPedidosPorStatus
{
    public class ObtemPedidosPorStatusAdapter : IObtemPedidosPorStatusAdapter
    {
        private readonly IObtemPedidosPorStatusUseCase _useCase;

        public ObtemPedidosPorStatusAdapter(IObtemPedidosPorStatusUseCase useCase)
        {
            _useCase = useCase;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ObtemPedidosPorStatusResponse))]
        public async Task<ObtemPedidosPorStatusResponse> Listar(EnumStatusPedido statusId)
        {
            var lista = await _useCase.Execute(statusId);

            var model = new List<PedidoModel>();
            lista.ToList().ForEach(f => model.Add(new PedidoModel(f)));

            return new ObtemPedidosPorStatusResponse(model);
        }
    }
}
