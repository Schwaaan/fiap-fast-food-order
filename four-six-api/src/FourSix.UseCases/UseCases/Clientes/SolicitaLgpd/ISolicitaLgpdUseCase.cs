using FourSix.Domain.Entities.ClienteAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourSix.UseCases.UseCases.Clientes.SolicitaLgpd
{
    public interface ISolicitaLgpdUseCase
    {
        Task<SolicitacaoLgpd> Execute(string cpf, string nomeCompleto);
    }
}
