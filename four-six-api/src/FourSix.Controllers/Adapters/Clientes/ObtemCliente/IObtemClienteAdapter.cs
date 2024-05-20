namespace FourSix.Controllers.Adapters.Clientes.ObtemCliente
{
    public interface IObtemClienteAdapter
    {
        Task<ObtemClienteResponse> Obter(string cpf);
    }
}
