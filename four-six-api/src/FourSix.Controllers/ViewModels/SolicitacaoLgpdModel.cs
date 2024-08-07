using FourSix.Domain.Entities.ClienteAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourSix.Controllers.ViewModels
{
    public class SolicitacaoLgpdModel
    {
        public SolicitacaoLgpdModel(SolicitacaoLgpd solicitacaoLgpd)
        {
            Id = solicitacaoLgpd.Id;
            Cpf = solicitacaoLgpd.Cpf;
            Nome = solicitacaoLgpd.Nome;
        }
        public Guid Id { get; }
        public string Cpf { get; }
        public string Nome { get; }
    }
}
