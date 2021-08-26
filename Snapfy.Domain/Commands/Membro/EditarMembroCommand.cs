using Flunt.Validations;

namespace Shoalace.Domain.Commands.Grupo
{
    public class EditarMembroCommand : Command
    {
        public long Id { get; set; }
        public long UsuarioId { get; set; }
        public long GrupoId { get; set; }
        public bool Admin { get; set; }

        public override void Validate()
        {
            AddNotifications(new Contract()
                .AreNotEquals(Id, 0, "Membro.Id", "Membro é obrigatório.")
                .AreNotEquals(UsuarioId, 0, "Membro.UsuarioId", "Usuario é obrigatório.")
                .AreNotEquals(GrupoId, 0, "Membro.GrupoId", "Grupo é obrigatório.")
                );
        }
    }
}
