using FourSix.Domain.Entities.ClienteAggregate;

namespace FourSix.UseCases.UseCases.Clientes.ObtemClientes
{
    public interface IObtemClientesUseCase
    {
        Task<ICollection<Cliente>> Execute();
    }
}
