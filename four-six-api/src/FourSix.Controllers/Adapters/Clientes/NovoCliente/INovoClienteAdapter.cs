namespace FourSix.Controllers.Adapters.Clientes.NovoCliente
{
    public interface INovoClienteAdapter
    {
        Task<NovoClienteResponse> Inserir(NovoClienteRequest cliente);
    }
}
