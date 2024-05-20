using FourSix.Domain.Entities.ClienteAggregate;
using FourSix.UseCases.Interfaces;

namespace FourSix.UseCases.UseCases.Clientes.NovoCliente
{
    public class NovoClienteUseCase : INovoClienteUseCase
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IUnitOfWork _unitOfWork;

        public NovoClienteUseCase(
            IClienteRepository clienteRepository,
            IUnitOfWork unitOfWork)
        {
            _clienteRepository = clienteRepository;
            _unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public Task<Cliente> Execute(string cpf, string nomeCompleto, string email) =>
            InserirCliente(
                new Cliente(Guid.NewGuid(),
                cpf,
                nomeCompleto,
                email));

        private async Task<Cliente> InserirCliente(Cliente cliente)
        {
            if (_clienteRepository.Listar(q => q.Cpf == cliente.Cpf).Any())
            {
                throw new Exception("Cliente já existe");
            }

            await _clienteRepository
                 .Incluir(cliente)
                 .ConfigureAwait(false);

            await _unitOfWork
                .Save()
                .ConfigureAwait(false);

            return cliente;
        }
    }
}
