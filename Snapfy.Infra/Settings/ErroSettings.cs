using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shoalace.Domain.Entities;

namespace Shoalace.Infra.Settings
{
    public class ErroSettings : IEntityTypeConfiguration<Erro>
    {
        public void Configure(EntityTypeBuilder<Erro> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}
