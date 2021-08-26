using Flunt.Validations;

namespace Shoalace.Domain.Commands.Grupo
{
    public class NovoMembroCommand : Command
    {
        public long UsuarioId { get; set; }
        public long GrupoId { get; set; }
        public bool Admin { get; private set; }

        public override void Validate()
        {
            AddNotifications(new Contract()
                 .AreNotEquals(UsuarioId, 0, "Membro.UsuarioId", "Usuario é obrigatório.")
                 .AreNotEquals(GrupoId, 0, "Membro.GrupoId", "Grupo é obrigatório.")
                 );
        }
    }
}
