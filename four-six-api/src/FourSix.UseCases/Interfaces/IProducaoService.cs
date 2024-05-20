namespace FourSix.UseCases.Interfaces
{
    public interface IProducaoService
    {
        Task IniciarProducao(Guid pedidoId);
    }
}
