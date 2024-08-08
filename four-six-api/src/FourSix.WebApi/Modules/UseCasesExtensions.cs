using FourSix.UseCases.UseCases.Clientes.ExecutaLgpd;
using FourSix.UseCases.UseCases.Clientes.NovoCliente;
using FourSix.UseCases.UseCases.Clientes.ObtemCliente;
using FourSix.UseCases.UseCases.Clientes.ObtemClientes;
using FourSix.UseCases.UseCases.Clientes.SolicitaLgpd;
using FourSix.UseCases.UseCases.Pedidos.AlteraStatusPedido;
using FourSix.UseCases.UseCases.Pedidos.CancelaPedido;
using FourSix.UseCases.UseCases.Pedidos.NovoPedido;
using FourSix.UseCases.UseCases.Pedidos.ObtemPedido;
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
            #region [ Clientes ]
            services.AddScoped<INovoClienteUseCase, NovoClienteUseCase>();
            services.AddScoped<IObtemClienteUseCase, ObtemClienteUseCase>();
            services.AddScoped<IObtemClientesUseCase, ObtemClientesUseCase>();
            services.AddScoped<ISolicitaLgpdUseCase, SolicitaLgpdUseCase>();
            services.AddScoped<IExecutaLgpdUseCase, ExecutaLgpdUseCase>();
            #endregion

            #region [ Pedidos ]
            services.AddScoped<IAlteraStatusPedidoUseCase, AlteraStatusPedidoUseCase>();
            services.AddScoped<ICancelaPedidoUseCase, CancelaPedidoUseCase>();
            services.AddScoped<INovoPedidoUseCase, NovoPedidoUseCase>();
            services.AddScoped<IObtemPedidosPorStatusUseCase, ObtemPedidosPorStatusUseCase>();
            services.AddScoped<IObtemPedidosUseCase, ObtemPedidosUseCase>();
            services.AddScoped<IObtemPedidoUseCase, ObtemPedidoUseCase>();
            #endregion

            return services;
        }
    }
}
