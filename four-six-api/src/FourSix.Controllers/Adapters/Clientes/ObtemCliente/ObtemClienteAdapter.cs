using FourSix.Controllers.ViewModels;
using FourSix.UseCases.UseCases.Clientes.ObtemCliente;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FourSix.Controllers.Adapters.Clientes.ObtemCliente
{
    public class ObtemClienteAdapter : IObtemClienteAdapter
    {
        private readonly IObtemClienteUseCase _useCase;

        public ObtemClienteAdapter(IObtemClienteUseCase useCase)
        {
            this._useCase = useCase;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ObtemClienteResponse))]
        public async Task<ObtemClienteResponse> Obter(string cpf)
        {
            var model = new ClienteModel(await _useCase.Execute(cpf));

            return new ObtemClienteResponse(model);
        }
    }
}
