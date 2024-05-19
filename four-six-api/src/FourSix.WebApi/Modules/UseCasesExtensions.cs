using FourSix.UseCases.UseCases.Pedidos.AlteraStatusPedido;
using FourSix.UseCases.UseCases.Pedidos.CancelaPedido;
using FourSix.UseCases.UseCases.Pedidos.NovoPedido;
using FourSix.UseCases.UseCases.Pedidos.ObtemPedidos;
using FourSix.UseCases.UseCases.Pedidos.ObtemPedidosPorStatus;
using System.Diagnostics.CodeAnalysis;

namespace FourSix.WebApi.Modules
{
    [ExcludeFromCodeCoverage]
    public static class UseCasesExtensions
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            #region [ Pedidos ]
            services.AddScoped<IAlteraStatusPedidoUseCase, AlteraStatusPedidoUseCase>();
            services.AddScoped<ICancelaPedidoUseCase, CancelaPedidoUseCase>();
            services.AddScoped<INovoPedidoUseCase, NovoPedidoUseCase>();
            services.AddScoped<IObtemPedidosPorStatusUseCase, ObtemPedidosPorStatusUseCase>();
            services.AddScoped<IObtemPedidosUseCase, ObtemPedidosUseCase>();
            #endregion

            return services;
        }
    }
}
