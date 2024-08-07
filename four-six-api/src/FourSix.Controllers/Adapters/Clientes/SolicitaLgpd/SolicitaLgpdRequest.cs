using Swashbuckle.AspNetCore.Annotations;

namespace FourSix.Controllers.Adapters.Clientes.SolicitaLgpd
{
    [SwaggerSchema(Required = new[] { "cpf", "nomeCompleto" })]
    public class SolicitaLgpdRequest
    {
        /// <summary>
        /// CPF do cliente
        /// </summary>
        /// <example>12345678921</example>
        [SwaggerSchema(Nullable = false)]
        public string Cpf { get; set; }
        /// <summary>
        /// Nome Completo do Cliente
        /// </summary>
        /// <example>João da Silva Gomes</example>
        [SwaggerSchema(Nullable = false)]
        public string NomeCompleto { get; set; }
    }
}
