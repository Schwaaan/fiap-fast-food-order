using FourSix.Controllers.ViewModels;
using FourSix.UseCases.UseCases.Pedidos.ObtemPedido;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FourSix.Controllers.Adapters.Pedidos.ObtemPedido
{
    public class ObtemPedidoAdapter : IObtemPedidoAdapter
    {
        private readonly IObtemPedidoUseCase _useCase;

        public ObtemPedidoAdapter(IObtemPedidoUseCase useCase)
        {
            _useCase = useCase;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ObtemPedidoResponse))]
        public async Task<ObtemPedidoResponse> Obter(Guid id)
        {
            var pedido = await _useCase.Execute(id);

            return new ObtemPedidoResponse(new PedidoModel(pedido));
        }
    }
}
