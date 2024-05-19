using FourSix.Controllers.Adapters.Pedidos.AlteraStatusPedido;
using FourSix.Controllers.Adapters.Pedidos.CancelaPedido;
using FourSix.Controllers.Adapters.Pedidos.NovoPedido;
using FourSix.Controllers.Adapters.Pedidos.ObtemPedidos;
using FourSix.Controllers.Adapters.Pedidos.ObtemPedidosPorStatus;
using System.Diagnostics.CodeAnalysis;

namespace FourSix.WebApi.Modules
{
    [ExcludeFromCodeCoverage]
    public static class AdaptersExtensions
    {
        public static IServiceCollection AddAdapters(this IServiceCollection services)
        {
            #region [ Pedidos ]
            services.AddScoped<IAlteraStatusPedidoAdapter, AlteraStatusPedidoAdapter>();
            services.AddScoped<ICancelaPedidoAdapter, CancelaPedidoAdapter>();
            services.AddScoped<INovoPedidoAdapter, NovoPedidoAdapter>();
            services.AddScoped<IObtemPedidosPorStatusAdapter, ObtemPedidosPorStatusAdapter>();
            services.AddScoped<IObtemPedidosAdapter, ObtemPedidosAdapter>();
            #endregion

            return services;
        }
    }
}
