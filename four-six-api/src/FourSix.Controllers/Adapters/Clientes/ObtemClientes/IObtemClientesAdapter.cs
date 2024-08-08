namespace FourSix.Controllers.Adapters.Clientes.ObtemClientes
{
    public interface IObtemClientesAdapter
    {
        Task<ObtemClientesResponse> Listar();
    }
}
