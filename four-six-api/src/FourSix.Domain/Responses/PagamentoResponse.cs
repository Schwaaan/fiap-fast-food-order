namespace FourSix.Domain.Requests
{
    public class PagamentoWrapper
    {
        public PagamentoResponse Pagamento { get; set; }
    }
    public class PagamentoResponse
    {
        public Guid Id { get; set; }
        public Guid PedidoId { get; set; }
        public string CodigoQR { get; set; }
        public decimal ValorPedido { get; set; }
        public decimal Desconto { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal ValorPago { get; set; }
        public DateTime DataAtualizacao { get; set; }
    }
}
