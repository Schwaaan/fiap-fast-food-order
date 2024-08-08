using FourSix.Controllers.Adapters.Clientes.ExecutaLgpd;
using FourSix.Controllers.Adapters.Clientes.NovoCliente;
using FourSix.Controllers.Adapters.Clientes.ObtemCliente;
using FourSix.Controllers.Adapters.Clientes.ObtemClientes;
using FourSix.Controllers.Adapters.Clientes.SolicitaLgpd;
using FourSix.Controllers.Adapters.Pedidos.AlteraStatusPedido;
using FourSix.Controllers.Adapters.Pedidos.CancelaPedido;
using FourSix.Controllers.Adapters.Pedidos.NovoPedido;
using FourSix.Controllers.Adapters.Pedidos.ObtemPedido;
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
            #region [ Clientes ]
            services.AddScoped<INovoClienteAdapter, NovoClienteAdapter>();
            services.AddScoped<IObtemClienteAdapter, ObtemClienteAdapter>();
            services.AddScoped<ISolicitaLgpdAdapter, SolicitaLgpdAdapter>();
            services.AddScoped<IExecutaLgpdAdapter, ExecutaLgpdAdapter>();
            services.AddScoped<IObtemClientesAdapter, ObtemClientesAdapter>();
            #endregion

            #region [ Pedidos ]
            services.AddScoped<IAlteraStatusPedidoAdapter, AlteraStatusPedidoAdapter>();
            services.AddScoped<ICancelaPedidoAdapter, CancelaPedidoAdapter>();
            services.AddScoped<INovoPedidoAdapter, NovoPedidoAdapter>();
            services.AddScoped<IObtemPedidosPorStatusAdapter, ObtemPedidosPorStatusAdapter>();
            services.AddScoped<IObtemPedidosAdapter, ObtemPedidosAdapter>();
            services.AddScoped<IObtemPedidoAdapter, ObtemPedidoAdapter>();
            #endregion

            return services;
        }
    }
}
