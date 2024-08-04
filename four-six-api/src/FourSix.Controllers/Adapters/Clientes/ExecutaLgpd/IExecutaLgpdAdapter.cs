using FourSix.Controllers.Adapters.Clientes.NovoCliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourSix.Controllers.Adapters.Clientes.ExecutaLgpd
{
    public interface IExecutaLgpdAdapter
    {
        Task<ExecutaLgpdResponse> ExecutarLgpd(Guid solicitacaoLgpdId);
    }
}
