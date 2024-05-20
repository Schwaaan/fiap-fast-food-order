using FourSix.Domain.Entities.ClienteAggregate;
using FourSix.UseCases.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FourSix.Controllers.Gateways.Repositories
{
    public class ClienteRepository : BaseRepository<Cliente>, IClienteRepository
    {
        public ClienteRepository(DbContext context) : base(context)
        {
        }
    }
}
