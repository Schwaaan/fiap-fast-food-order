using FourSix.Controllers.ViewModels;

namespace FourSix.Controllers.Adapters.Clientes.ObtemClientes
{
    public class ObtemClientesResponse
    {
        public ObtemClientesResponse(ICollection<ClienteModel> clientesModel) => Clientes = clientesModel;
        public ICollection<ClienteModel> Clientes { get; }
    }
}
