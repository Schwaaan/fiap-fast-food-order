using FourSix.Controllers.ViewModels;
using FourSix.UseCases.UseCases.Pedidos.ObtemPedidos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FourSix.Controllers.Adapters.Pedidos.ObtemPedidos
{
    public class ObtemPedidosAdapter
        : IObtemPedidosAdapter
    {
        private readonly IObtemPedidosUseCase _useCase;

        public ObtemPedidosAdapter(IObtemPedidosUseCase useCase)
        {
            _useCase = useCase;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ObtemPedidosResponse))]
        public async Task<ObtemPedidosResponse> Listar()
        {
            var lista = await _useCase.Execute();

            var model = new List<PedidoModel>();
            lista.ToList().ForEach(f => model.Add(new PedidoModel(f)));

            return new ObtemPedidosResponse(model);
        }
    }
}
