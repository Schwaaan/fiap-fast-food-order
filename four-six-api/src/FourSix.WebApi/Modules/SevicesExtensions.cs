using FourSix.Controllers.Gateways.Integrations.ProdutoIntegration;
using FourSix.Controllers.Gateways.Integrations.ProducaoIntegration;
using FourSix.UseCases.Interfaces;
using System.Diagnostics.CodeAnalysis;
using FourSix.Controllers.Gateways.Integrations.PagamentoIntegration;

namespace FourSix.WebApi.Modules
{
    [ExcludeFromCodeCoverage]
    public static class SevicesExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            #region [ Integrations ]
            services.AddScoped<IProdutoService, ProdutoService>();
            services.AddScoped<IProducaoService, ProducaoService>();
            services.AddScoped<IPagamentoService, PagamentoService>();
            #endregion

            return services;
        }
    }
}
