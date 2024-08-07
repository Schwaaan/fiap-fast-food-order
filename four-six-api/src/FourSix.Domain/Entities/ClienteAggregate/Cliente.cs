namespace FourSix.Domain.Entities.ClienteAggregate
{
    public class Cliente : BaseEntity, IAggregateRoot, IBaseEntity
    {
        public Cliente(){}    
        public Cliente(Guid id, string cpf, string nome, string email)
        {
            this.Id = id;
            this.Cpf = cpf;
            this.Nome = nome;
            this.Email = email;
        }
        public string Cpf { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public DateTime? DataAnonimizado { get; private set; }
        public void Anonimizar()
        {
            Cpf = Guid.NewGuid().ToString().Substring(0, 11);
            Nome = Guid.NewGuid().ToString();
            Email = Guid.NewGuid().ToString();
            DataAnonimizado = DateTime.Now;
        }
    }
}
