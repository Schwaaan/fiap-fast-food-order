using FourSix.Domain.Entities.PedidoAggregate;
using FourSix.UseCases.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FourSix.Controllers.Gateways.Repositories
{
    public class PedidoCheckoutRepository : BaseRepository<PedidoCheckout, EnumStatusPedido>, IPedidoCheckoutRepository
    {
        public PedidoCheckoutRepository(DbContext context) : base(context)
        {
        }
    }
}
