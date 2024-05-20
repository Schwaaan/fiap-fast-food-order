namespace FourSix.Controllers.Gateways.Integrations
{
    public class ProdutoWrapper
    {
        public ProdutoRequest Produto { get; set; }
    }

    public class ProdutoRequest
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
    }
}
