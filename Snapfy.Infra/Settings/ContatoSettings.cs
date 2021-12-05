using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shoalace.Domain.Entities;

namespace Shoalace.Infra.Settings
{
    public class ContatoSettings : IEntityTypeConfiguration<Contato>
    {
        public void Configure(EntityTypeBuilder<Contato> builder)
        {
            builder.HasKey(u => u.Id);
        }
    }
}
