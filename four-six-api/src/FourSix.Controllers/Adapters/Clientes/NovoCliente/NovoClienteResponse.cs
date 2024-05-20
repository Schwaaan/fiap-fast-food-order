using FourSix.Controllers.ViewModels;

namespace FourSix.Controllers.Adapters.Clientes.NovoCliente
{
    public class NovoClienteResponse
    {
        public NovoClienteResponse(ClienteModel clienteModel) => this.Cliente = clienteModel;
        public ClienteModel Cliente { get; }
    }
}
