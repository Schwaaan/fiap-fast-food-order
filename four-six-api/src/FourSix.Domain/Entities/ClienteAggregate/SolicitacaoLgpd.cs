namespace FourSix.Domain.Entities.ClienteAggregate
{
    public class SolicitacaoLgpd : BaseEntity, IAggregateRoot, IBaseEntity
    {
        public SolicitacaoLgpd() { }
        public SolicitacaoLgpd(Guid id, string cpf, string nome)
        {
            this.Id = id;
            this.Cpf = cpf;
            this.Nome = nome;
            this.DataSolicitacao = DateTime.Now;
        }
        public string Cpf { get; }
        public string Nome { get; }
        public DateTime? DataSolicitacao { get; }
        public DateTime? DataAtendimento { get; private set; }
        public void Anonimizar()
        {
            DataAtendimento = DateTime.Now;
        }
    }
}
