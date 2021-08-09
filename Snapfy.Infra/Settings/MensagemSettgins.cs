using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shoalace.Domain.Entities;

namespace Shoalace.Infra.Settings
{
    public class MensagemSettings : IEntityTypeConfiguration<Mensagem>
    {
        public void Configure(EntityTypeBuilder<Mensagem> builder)
        {
            builder.HasKey(m => m.Id);
            builder.HasOne(m => m.Usuario).WithMany().OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(m => m.UsuarioDestino).WithMany().OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(m => m.StatusMensagens).WithOne(s => s.Mensagem);
        }
    }
}
