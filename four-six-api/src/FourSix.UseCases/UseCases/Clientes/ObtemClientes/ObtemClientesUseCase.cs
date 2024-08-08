using FourSix.Domain.Entities.ClienteAggregate;
using FourSix.UseCases.Interfaces;

namespace FourSix.UseCases.UseCases.Clientes.ObtemClientes
{
    public class ObtemClientesUseCase : IObtemClientesUseCase
    {
        private readonly IClienteRepository _clienteRepository;

        public ObtemClientesUseCase(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public Task<ICollection<Cliente>> Execute() => ListarClientes();

        private async Task<ICollection<Cliente>> ListarClientes()
        {
            var clientes = _clienteRepository.Listar()
                .OrderByDescending(x => x.Nome).ToList();

            return clientes;
        }
    }
}
