using FourSix.Domain.Entities.PedidoAggregate;

namespace FourSix.Controllers.ViewModels
{
    public class PedidoModel
    {
        public PedidoModel(Pedido pedido)
        {
            Id = pedido.Id;
            NumeroPedido = $"{pedido.NumeroPedido:00000}" ;
            ClienteId = pedido.ClienteId;
            DataPedido = pedido.DataPedido;
            StatusId = pedido.StatusId;
            TotalItens = pedido.TotalItens;
            ValorTotal = pedido.ValorTotal;
            Itens = pedido.Itens.Select(s => new PedidoItemModel(s)).ToList();
            Checkout = pedido.HistoricoCheckout.Select(s => new PedidoCheckoutModel(s)).ToList();
        }

        public Guid Id { get; }
        public string NumeroPedido { get; }
        public Guid? ClienteId { get; }
        public DateTime DataPedido { get; }
        public EnumStatusPedido StatusId { get; }
        public int TotalItens { get; }
        public decimal ValorTotal { get; }
        public List<PedidoItemModel> Itens { get; }
        public List<PedidoCheckoutModel> Checkout { get; }
    }
}
