using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shoalace.Domain.Entities;
using Shoalace.Domain.Validations;

namespace Shoalace.Infra.Settings
{
    public class ContatoSettings : IEntityTypeConfiguration<Contato>
    {
        public void Configure(EntityTypeBuilder<Contato> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(U => U.Numero).IsRequired().HasMaxLength(ContatoValidation.NUMERO_MAXLENGTH);
        }
    }
}
