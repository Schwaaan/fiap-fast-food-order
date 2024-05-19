using Microsoft.OpenApi.Models;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace FourSix.WebApi.Modules.Commons
{
    [ExcludeFromCodeCoverage]
    public static class SwaggerConfiguration
    {
        public static void AddSwaggerConfig(this IServiceCollection services)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);

            services.AddSwaggerGen(s =>
            {
                s.EnableAnnotations();
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = fvi.ProductVersion,
                    Title = "Four Six Order API",
                    Description = $"API de Integração - Four Six Orders",
                    Contact = new OpenApiContact
                    {
                        Name = "TI FourSix - Grupo 46"
                    },
                    TermsOfService = null,
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                s.IncludeXmlComments(xmlPath);
            });

        }
    }
}
