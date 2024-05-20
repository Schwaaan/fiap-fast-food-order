using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourSix.Controllers.Gateways.Integrations
{
    public interface IProdutoService
    {
        Task<ProdutoRequest> GetProduto(Guid id);
    }
}
