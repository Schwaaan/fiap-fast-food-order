using FourSix.Domain.Entities.ClienteAggregate;
using FourSix.UseCases.Interfaces;

namespace FourSix.UseCases.UseCases.Clientes.ObtemCliente
{
    public class ObtemClienteUseCase : IObtemClienteUseCase
    {
        private readonly IClienteRepository _clienteRepository;

        public ObtemClienteUseCase(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public Task<Cliente> Execute(string cpf) => ObterCliente(cpf);

        private async Task<Cliente> ObterCliente(string cpf)
        {
            var cliente = _clienteRepository
                .Listar(q => q.Cpf == cpf).FirstOrDefault();

            if (cliente == null)
            {
                throw new Exception("Cliente não encontrado");
            }

            return cliente;
        }
    }
}