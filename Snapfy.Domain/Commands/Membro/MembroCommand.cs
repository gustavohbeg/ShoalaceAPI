using Flunt.Notifications;
using Flunt.Validations;
using Shoalace.Domain.Validations;

namespace Shoalace.Domain.Commands.Grupo
{
    public class MembroCommand : Command
    {
        public long UsuarioId { get; set; }
        public bool Admin { get; set; }

        public override void Validate() =>
              AddNotifications(new Contract<Notification>[]
            {
                MembroValidation.ValidateUsuarioId(UsuarioId)
            });
    }
}
