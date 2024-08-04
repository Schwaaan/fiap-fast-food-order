using FourSix.Controllers.Adapters.Clientes.NovoCliente;
using FourSix.Controllers.ViewModels;
using FourSix.UseCases.UseCases.Clientes.NovoCliente;
using FourSix.UseCases.UseCases.Clientes.SolicitaLgpd;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FourSix.Controllers.Adapters.Clientes.SolicitaLgpd
{
    public class SolicitaLgpdAdapter : ISolicitaLgpdAdapter
    {
        private readonly ISolicitaLgpdUseCase _useCase;

        public SolicitaLgpdAdapter(ISolicitaLgpdUseCase useCase)
        {
            _useCase = useCase;
        }

        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(NovoClienteResponse))]
        public async Task<SolicitaLgpdResponse> Inserir(SolicitaLgpdRequest solicitaLgpd)
        {
            var model = new SolicitacaoLgpdModel(await _useCase.Execute(solicitaLgpd.Cpf, solicitaLgpd.NomeCompleto));

            return new SolicitaLgpdResponse(model);
        }
    }
}
