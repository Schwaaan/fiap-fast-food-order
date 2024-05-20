using FourSix.Controllers.Gateways.Integrations;
using System.Diagnostics.CodeAnalysis;

namespace FourSix.WebApi.Modules
{
    [ExcludeFromCodeCoverage]
    public static class SevicesExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            #region [ Integrations ]
            services.AddScoped<IProdutoService, ProdutoService>();
            #endregion

            return services;
        }
    }
}
