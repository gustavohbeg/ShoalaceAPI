using Flunt.Notifications;
using Flunt.Validations;
using Shoalace.Domain.Validations;

namespace Shoalace.Domain.Commands.Grupo
{
    public class NovoMembroCommand : Command
    {
        public long UsuarioId { get; set; }
        public long GrupoId { get; set; }
        public bool Admin { get; private set; }

        public override void Validate() =>
              AddNotifications(new Contract<Notification>[]
            {
                MembroValidation.ValidateUsuarioId(UsuarioId),
                MembroValidation.ValidateGrupoId(GrupoId),
            });
    }
}
