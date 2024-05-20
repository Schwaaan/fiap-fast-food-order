using FourSix.Controllers.Gateways.Integrations.ProdutoIntegration;
using FourSix.Domain.Requests;
using FourSix.UseCases.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FourSix.Controllers.Gateways.Integrations.PagamentoIntegration
{
    public class PagamentoService : IPagamentoService
    {
        private readonly HttpClient _httpClient;
        public PagamentoService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            httpClient.BaseAddress = new Uri(configuration.GetValue<string>("ApiConfiguration:PagamentoApiUrl"));
        }

        public async Task<PagamentoResponse> GerarPagamento(Guid pedidoId, decimal valor)
        {
            var request = new StringContent(JsonSerializer.Serialize(new
            {
                PedidoId = pedidoId,
                ValorPedido = valor
            }), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"/pagamentos", request);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var pagamento = JsonSerializer.Deserialize<PagamentoWrapper>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return pagamento.Pagamento;
        }
    }
}
