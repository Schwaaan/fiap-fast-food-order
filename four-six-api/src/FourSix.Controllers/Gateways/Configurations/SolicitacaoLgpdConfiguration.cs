using FourSix.Domain.Entities.ClienteAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FourSix.Controllers.Gateways.Configurations
{
    public class SolicitacaoLgpdConfiguration : IEntityTypeConfiguration<SolicitacaoLgpd>
    {

        public void Configure(EntityTypeBuilder<SolicitacaoLgpd> builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.ToTable("SolicitacaoLgpd");
            builder.HasKey(e => e.Id);
            builder.Property(b => b.Id)
                .IsRequired()
                .ValueGeneratedOnAdd()
                .UsePropertyAccessMode(PropertyAccessMode.FieldDuringConstruction);

            builder.Property(b => b.Cpf)
                .IsRequired()
                .HasMaxLength(11)
                .UsePropertyAccessMode(PropertyAccessMode.FieldDuringConstruction);

            builder.Property(b => b.Nome)
                .IsRequired()
                .HasMaxLength(50)
                .UsePropertyAccessMode(PropertyAccessMode.FieldDuringConstruction);

            builder.Property(b => b.DataSolicitacao)
           .IsRequired()
           .UsePropertyAccessMode(PropertyAccessMode.FieldDuringConstruction);

            builder.Property(b => b.DataAtendimento)
           .UsePropertyAccessMode(PropertyAccessMode.FieldDuringConstruction);

        }
    }
}