namespace FourSix.Domain.Entities.PedidoAggregate
{
    public class PedidoItem
    {
        public PedidoItem() { }
        public PedidoItem(Guid pedidoId, Guid produtoId, decimal valorUnitario, int quantidade, string? observacao)
        {
            PedidoId = pedidoId;
            ProdutoId = produtoId;
            ValorUnitario = valorUnitario;
            Quantidade = quantidade;
            Observacao = observacao;
        }

        public Guid PedidoId { get; }
        public Guid ProdutoId { get; }
        public decimal ValorUnitario { get; }
        public int Quantidade { get; private set; }
        public string? Observacao { get; }

        public void AdicionarQuantidade(int quantidade)
        {
            Quantidade += quantidade;
        }
    }
}
