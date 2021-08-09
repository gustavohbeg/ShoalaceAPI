using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shoalace.Domain.Entities;

namespace Shoalace.Infra.Settings
{
    public class MembroSettings : IEntityTypeConfiguration<Membro>
    {
        public void Configure(EntityTypeBuilder<Membro> builder)
        {
            builder.HasKey(m => m.Id);
            //builder.HasOne(m => m.Grupo).WithMany().OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(m => m.Usuario).WithMany().OnDelete(DeleteBehavior.Restrict);
        }
    }
}
