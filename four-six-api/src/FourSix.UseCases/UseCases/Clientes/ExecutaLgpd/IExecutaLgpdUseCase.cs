using FourSix.Domain.Entities.ClienteAggregate;

namespace FourSix.UseCases.UseCases.Clientes.ExecutaLgpd
{
    public interface IExecutaLgpdUseCase
    {
        Task<Cliente> Execute(Guid solicitacaoLgpdId);
    }
}
