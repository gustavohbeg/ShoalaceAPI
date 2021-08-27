using Flunt.Validations;

namespace Shoalace.Domain.Commands.Grupo
{
    public class MembroCommand : Command
    {
        public long UsuarioId { get; set; }
        public bool Admin { get; set; }

        public override void Validate()
        {
            AddNotifications(new Contract()
                 .AreNotEquals(UsuarioId, 0, "Membro.UsuarioId", "Usuario é obrigatório.")
                 );
        }
    }
}
