using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shoalace.Domain.Entities;

namespace Shoalace.Infra.Settings
{
    public class AcessoSettings : IEntityTypeConfiguration<Acesso>
    {
        public void Configure(EntityTypeBuilder<Acesso> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Codigo).IsRequired().HasMaxLength(4);
        }
    }
}
