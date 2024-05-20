using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace FourSix.Controllers.Gateways.Integrations.ProdutoIntegration
{
    public class ProdutoService : IProdutoService
    {
        private readonly HttpClient _httpClient;
        public ProdutoService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            httpClient.BaseAddress = new Uri(configuration.GetValue<string>("ApiConfiguration:ProdutoApiUrl"));
        }

        public async Task<ProdutoRequest> GetProduto(Guid id)
        {
            var response = await _httpClient.GetAsync($"/produtos/{id}");
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
