using FourSix.Controllers.ViewModels;

namespace FourSix.Controllers.Adapters.Clientes.SolicitaLgpd
{
    public class SolicitaLgpdResponse
    {
        public SolicitaLgpdResponse(SolicitacaoLgpdModel solicitacaoLgpdModel) => this.SolicitacaoLgpd = solicitacaoLgpdModel;
        public SolicitacaoLgpdModel SolicitacaoLgpd { get; }
    }
}
