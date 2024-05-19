using FourSix.Domain.Entities.PedidoAggregate;

namespace FourSix.Controllers.ViewModels
{
    public class PedidoItemModel
    {
        public PedidoItemModel(PedidoItem item)
        {
            PedidoId = item.PedidoId;
            ItemPedidoId = item.ProdutoId;
            ValorUnitario = item.ValorUnitario;
            Quantidade = item.Quantidade;
            Observacao = item.Observacao;
        }

        public Guid PedidoId { get; }
        public Guid ItemPedidoId { get; }
        public decimal ValorUnitario { get; }
        public int Quantidade { get; }
        public string? Observacao { get; }
    }
}
