using Flunt.Validations;

namespace Shoalace.Domain.Commands.Grupo
{
    public class EditarGrupoCommand : Command
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public byte? Foto { get; set; }
        public long UsuarioId { get; set; }

        public override void Validate()
        {
            AddNotifications(new Contract()
                .AreNotEquals(Id, 0, "Grupo.Id", "Grupo é obrigatório.")
                .IsNotNullOrEmpty(Nome, "Grupo.Nome", "Nome é obrigatório.")
                .AreNotEquals(UsuarioId, 0, "Grupo.UsuarioId", "Usuario é obrigatório.")
                );
        }
    }
}
