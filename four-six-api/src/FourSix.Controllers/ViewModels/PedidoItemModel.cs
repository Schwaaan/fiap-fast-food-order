using FourSix.Controllers.Gateways.Integrations;
using FourSix.Domain.Entities.PedidoAggregate;

namespace FourSix.Controllers.ViewModels
{
    public class PedidoItemModel
    {
        public PedidoItemModel(PedidoItem item, ProdutoRequest produto)
        {
            PedidoId = item.PedidoId;
            ItemPedidoId = item.ProdutoId;
            ValorUnitario = item.ValorUnitario;
            Quantidade = item.Quantidade;
            Observacao = item.Observacao;
            Produto = new ProdutoModel(produto);
        }

        public Guid PedidoId { get; }
        public Guid ItemPedidoId { get; }
        public decimal ValorUnitario { get; }
        public int Quantidade { get; }
        public string? Observacao { get; }
        public ProdutoModel Produto { get; set; }
    }
}
