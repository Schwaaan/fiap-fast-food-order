using FourSix.Domain.Entities.ClienteAggregate;

namespace FourSix.UseCases.UseCases.Clientes.ObtemCliente
{
    public interface IObtemClienteUseCase
    {
        Task<Cliente> Execute(string cpf);
    }
}
