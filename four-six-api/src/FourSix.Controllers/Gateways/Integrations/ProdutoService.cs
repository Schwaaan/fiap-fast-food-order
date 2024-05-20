using System.Text.Json;

namespace FourSix.Controllers.Gateways.Integrations
{
    public class ProdutoService : IProdutoService
    {
        private readonly HttpClient _httpClient;
        public ProdutoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ProdutoRequest> GetProduto(Guid id)
        {
            var response = await _httpClient.GetAsync($"http://api-svc-product-foursix:30010/produtos/{id}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var produto = JsonSerializer.Deserialize<ProdutoWrapper>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return produto.Produto;
        }
    }
}
