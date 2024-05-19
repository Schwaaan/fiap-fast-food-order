using FourSix.Controllers.Gateways.DataAccess;
using FourSix.Controllers.Gateways.Repositories;
using FourSix.UseCases.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Net.Security;
using System.Security.Authentication;

namespace FourSix.WebApi.Modules
{
    [ExcludeFromCodeCoverage]
    public static class DatabaseExtensions
    {
        static string envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        static readonly IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
            .AddJsonFile("appsettings.json", false)
            .AddJsonFile($"appsettings.{envName}.json", true)
            .AddEnvironmentVariables()
            .Build();

        public static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            services.AddDbContext<Context>(option =>
            {
                option.UseSqlServer(ReadDefaultConnectionStringFromAppSettings("PersistenceModule:SQLServerConnection"));
            });

            services.AddStackExchangeRedisCache(options =>
            {
                options.ConfigurationOptions = new StackExchange.Redis.ConfigurationOptions();
                options.ConfigurationOptions.AbortOnConnectFail = false;
                options.ConfigurationOptions.AllowAdmin = true;
                options.ConfigurationOptions.Ssl = true;
                options.ConfigurationOptions.SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls13;
                options.ConfigurationOptions.EndPoints.Add(ReadDefaultConnectionStringFromAppSettings("CacheModule:RedisConnection"));

                options.ConfigurationOptions.CertificateValidation += (sender, certificate, chain, sslPolicyErrors) =>
                {
                    if (sslPolicyErrors == SslPolicyErrors.None)
                    {
                        return true;
                    }

                    if (sslPolicyErrors == SslPolicyErrors.RemoteCertificateNameMismatch)
                    {
                        return true;
                    }

                    return false;
                };
            }
            );

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<IPedidoCheckoutRepository, PedidoCheckoutRepository>();
            services.AddScoped<DbContext, Context>();

            return services;
        }

        private static string ReadDefaultConnectionStringFromAppSettings(string value)
        {
            string connectionString = configuration.GetValue<string>(value);
            return connectionString;
        }
    }

}
