using FourSix.Domain.Entities.ClienteAggregate;

namespace FourSix.UseCases.Interfaces
{
    public interface IClienteRepository : IGetRepository<Cliente>, ISetRepository<Cliente>
    {
    }
}
