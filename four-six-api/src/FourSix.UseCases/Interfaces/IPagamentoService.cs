using FourSix.Domain.Requests;

namespace FourSix.UseCases.Interfaces
{
    public interface IPagamentoService
    {
        Task<PagamentoResponse> GerarPagamento(Guid pedidoId, decimal valor); 
    }
}
