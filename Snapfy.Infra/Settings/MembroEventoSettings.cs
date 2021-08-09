using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shoalace.Domain.Entities;

namespace Shoalace.Infra.Settings
{
    public class MembroEventoSettings : IEntityTypeConfiguration<MembroEvento>
    {
        public void Configure(EntityTypeBuilder<MembroEvento> builder)
        {
            builder.HasKey(m => m.Id);
            builder.HasOne(m => m.Usuario).WithMany().OnDelete(DeleteBehavior.Restrict);
        }
    }
}
