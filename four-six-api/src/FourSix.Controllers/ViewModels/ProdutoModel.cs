using FourSix.Controllers.Gateways.Integrations;

namespace FourSix.Controllers.ViewModels
{
    public class ProdutoModel
    {
        public ProdutoModel(ProdutoRequest produto)
        {
            Nome = produto.Nome;
            Preco = produto.Preco;
        }

        public string Nome { get; }
        public decimal Preco { get; }
    }
}
