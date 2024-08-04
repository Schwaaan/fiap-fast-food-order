using FourSix.Domain.Entities.ClienteAggregate;
using FourSix.UseCases.Interfaces;
using FourSix.UseCases.UseCases.Clientes.NovoCliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourSix.UseCases.UseCases.Clientes.SolicitaLgpd
{
    public class SolicitaLgpdUseCase : ISolicitaLgpdUseCase
    {
        private readonly ISolicitacaoLgpdRepository _solicitaLgpdRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SolicitaLgpdUseCase(
            ISolicitacaoLgpdRepository solicitaLgpdRepository,
            IUnitOfWork unitOfWork)
        {
            _solicitaLgpdRepository = solicitaLgpdRepository;
            _unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public Task<SolicitacaoLgpd> Execute(string cpf, string nomeCompleto) =>
            InserirSolicitacaoLgpd(
                new SolicitacaoLgpd(Guid.NewGuid(),
                cpf,
                nomeCompleto));

        private async Task<SolicitacaoLgpd> InserirSolicitacaoLgpd(SolicitacaoLgpd solicitacaoLgpd)
        {
            await _solicitaLgpdRepository
                 .Incluir(solicitacaoLgpd)
                 .ConfigureAwait(false);

            await _unitOfWork
                .Save()
                .ConfigureAwait(false);

            return solicitacaoLgpd;
        }
    }
}