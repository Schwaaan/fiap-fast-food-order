namespace FourSix.Domain.Entities.ClienteAggregate
{
    public class Cliente : BaseEntity, IAggregateRoot, IBaseEntity
    {
        public Cliente(){}    
        public Cliente(Guid id, string cpf, string nome, string email)
        {
            this.Id = Id;
            this.Cpf = cpf;
            this.Nome = nome;
            this.Email = email;
        }
        public string Cpf { get; }
        public string Nome { get; }
        public string Email { get; }
    }
}
