using FourSix.Controllers.ViewModels;
using FourSix.UseCases.UseCases.Clientes.ObtemClientes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FourSix.Controllers.Adapters.Clientes.ObtemClientes
{
    public class ObtemClientesAdapter : IObtemClientesAdapter
    {
        private readonly IObtemClientesUseCase _useCase;

        public ObtemClientesAdapter(IObtemClientesUseCase useCase)
        {
            _useCase = useCase;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ObtemClientesResponse))]
        public async Task<ObtemClientesResponse> Listar()
        {
            var lista = await _useCase.Execute();

            var model = new List<ClienteModel>();
            lista.ToList().ForEach(f => model.Add(new ClienteModel(f)));

            return new ObtemClientesResponse(model);
        }
    }
}
