using FourSix.UseCases.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace FourSix.Controllers.Gateways.Integrations.ProducaoIntegration
{
    public class ProducaoService : IProducaoService
    {
        private readonly HttpClient _httpClient;
        public ProducaoService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            httpClient.BaseAddress = new Uri(configuration.GetValue<string>("ApiConfiguration:ProducaoApiUrl"));
        }

        public async Task IniciarProducao(Guid id)
        {
            var content = new StringContent(JsonSerializer.Serialize(new { id }), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"/order/create", content);
            response.EnsureSuccessStatusCode();
        }
    }
}
