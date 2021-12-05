using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using Shoalace.Domain.Entities;
using Shoalace.Infra.Settings;

namespace Shoalace.Infra.Contexto
{
    public class ShoalaceContexto : DbContext
    {
        public ShoalaceContexto(DbContextOptions<ShoalaceContexto> options) : base(options) { }
        public ShoalaceContexto() { }

        //Ordem Alfabética
        public DbSet<Acesso> Acesso { get; set; }
        public DbSet<Contato> Contato { get; set; }
        public DbSet<Evento> Evento { get; set; }
        public DbSet<Erro> Erro { get; set; }
        public DbSet<Grupo> Grupo { get; set; }
        public DbSet<Membro> Membro { get; set; }
        public DbSet<MembroEvento> MembroEvento { get; set; }
        public DbSet<Mensagem> Mensagem { get; set; }
        public DbSet<StatusMensagem> StatusMensagem { get; set; }
        public DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Notification>();
            //Ordem Alfabética
            modelBuilder.ApplyConfiguration(new AcessoSettings());
            modelBuilder.ApplyConfiguration(new ContatoSettings());
            modelBuilder.ApplyConfiguration(new EventoSettings());
            modelBuilder.ApplyConfiguration(new ErroSettings());
            modelBuilder.ApplyConfiguration(new GrupoSettings());
            modelBuilder.ApplyConfiguration(new MembroSettings());
            modelBuilder.ApplyConfiguration(new MembroEventoSettings());
            modelBuilder.ApplyConfiguration(new MensagemSettings());
            modelBuilder.ApplyConfiguration(new StatusMensagemSettings());
            modelBuilder.ApplyConfiguration(new UsuarioSettings());

            base.OnModelCreating(modelBuilder);
        }
    }
}
