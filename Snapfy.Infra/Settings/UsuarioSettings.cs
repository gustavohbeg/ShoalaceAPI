using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shoalace.Domain.Entities;

namespace Shoalace.Infra.Settings
{
    public class UsuarioSettings : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Nome).IsRequired();
            builder.Property(u => u.Token).IsRequired();
            builder.HasMany(u => u.Contatos).WithOne(r => r.Usuario).HasForeignKey(r => r.UsuarioId);
            builder.HasMany(u => u.EContatos).WithOne(r => r.UsuarioContato).HasForeignKey(r => r.UsuarioContatoId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
