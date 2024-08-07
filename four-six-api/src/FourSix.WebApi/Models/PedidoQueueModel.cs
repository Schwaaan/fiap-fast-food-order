using FourSix.Domain.Entities.PedidoAggregate;

namespace FourSix.WebApi.Models
{
    public class PedidoQueueModel
    {
        public Guid Id { get; set; }
        public int StatusId { get; set; }
    }
}
