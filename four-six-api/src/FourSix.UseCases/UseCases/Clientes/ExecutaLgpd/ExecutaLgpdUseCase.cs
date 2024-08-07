using FourSix.Domain.Entities.ClienteAggregate;
using FourSix.UseCases.Interfaces;

namespace FourSix.UseCases.UseCases.Clientes.ExecutaLgpd
{
    public class ExecutaLgpdUseCase : IExecutaLgpdUseCase
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly ISolicitacaoLgpdRepository _solicitacaoLgpdRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ExecutaLgpdUseCase(
            IClienteRepository clienteRepository,
            IUnitOfWork unitOfWork,
            ISolicitacaoLgpdRepository solicitacaoLgpdRepository)
        {
            _clienteRepository = clienteRepository;
            _unitOfWork = unitOfWork;
            _solicitacaoLgpdRepository = solicitacaoLgpdRepository;
        }

        public Task<Cliente> Execute(Guid solicitacaoLgpdId) => Anonimizar(solicitacaoLgpdId);

        private async Task<Cliente> Anonimizar(Guid solicitacaoLgpdId)
        {
            var solicitacaoLgpd = _solicitacaoLgpdRepository.Listar(q => q.Id == solicitacaoLgpdId).FirstOrDefault();

            if (solicitacaoLgpd == null)
            {
                throw new Exception("Solicitação não encontrado");
            }

            var cliente = _clienteRepository.Listar(q => q.Cpf == solicitacaoLgpd.Cpf).FirstOrDefault();

            if (cliente == null)
            {
                throw new Exception("Cliente não encontrado");
            }

            cliente.Anonimizar();
            await _clienteRepository.Alterar(cliente);

            solicitacaoLgpd.Anonimizar();

            await _solicitacaoLgpdRepository
                 .Alterar(solicitacaoLgpd);

            await _unitOfWork
                .Save()
                .ConfigureAwait(false);

            return cliente;
        }
    }
}