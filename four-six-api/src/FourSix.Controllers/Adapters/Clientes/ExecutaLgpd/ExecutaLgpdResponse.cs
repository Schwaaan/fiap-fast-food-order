using FourSix.Controllers.ViewModels;

namespace FourSix.Controllers.Adapters.Clientes.ExecutaLgpd
{
    public class ExecutaLgpdResponse
    {
        public ExecutaLgpdResponse(ClienteModel clienteModel) => this.Cliente = clienteModel;
        public ClienteModel Cliente { get; }
    }
}
