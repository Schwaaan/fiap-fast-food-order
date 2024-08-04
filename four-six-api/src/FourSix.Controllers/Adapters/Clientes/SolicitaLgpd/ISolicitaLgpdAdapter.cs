namespace FourSix.Controllers.Adapters.Clientes.SolicitaLgpd
{
    public interface ISolicitaLgpdAdapter
    {
        Task<SolicitaLgpdResponse> Inserir(SolicitaLgpdRequest cliente);
    }
}
