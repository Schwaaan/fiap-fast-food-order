using FourSix.Controllers.ViewModels;
using FourSix.UseCases.UseCases.Clientes.ExecutaLgpd;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FourSix.Controllers.Adapters.Clientes.ExecutaLgpd
{
    public class ExecutaLgpdAdapter : IExecutaLgpdAdapter
    {
        private readonly IExecutaLgpdUseCase _useCase;

        public ExecutaLgpdAdapter(IExecutaLgpdUseCase useCase)
        {
            _useCase = useCase;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExecutaLgpdResponse))]
        public async Task<ExecutaLgpdResponse> ExecutarLgpd(Guid solicitacaoLgpdId)
        {
            var model = new ClienteModel(await _useCase.Execute(solicitacaoLgpdId));

            return new ExecutaLgpdResponse(model);
        }
    }
}
