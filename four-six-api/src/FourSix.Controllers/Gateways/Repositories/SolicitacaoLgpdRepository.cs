using FourSix.Domain.Entities.ClienteAggregate;
using FourSix.UseCases.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FourSix.Controllers.Gateways.Repositories
{
    public class SolicitacaoLgpdRepository : BaseRepository<SolicitacaoLgpd>, ISolicitacaoLgpdRepository
    {
        public SolicitacaoLgpdRepository(DbContext context) : base(context)
        {
        }
    }
}
