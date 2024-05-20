using FourSix.Controllers.ViewModels;

namespace FourSix.Controllers.Adapters.Clientes.ObtemCliente
{
    public class ObtemClienteResponse
    {
        public ObtemClienteResponse(ClienteModel clienteModel) => this.Cliente = clienteModel;
        public ClienteModel Cliente { get; }
    }
}
