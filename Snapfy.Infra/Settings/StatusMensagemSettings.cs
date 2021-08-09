using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shoalace.Domain.Entities;

namespace Shoalace.Infra.Settings
{
    public class StatusMensagemSettings : IEntityTypeConfiguration<StatusMensagem>
    {
        public void Configure(EntityTypeBuilder<StatusMensagem> builder)
        {
            builder.HasKey(s => s.Id);
            builder.HasOne(s => s.Membro).WithMany().OnDelete(DeleteBehavior.Restrict);
        }
    }
}
