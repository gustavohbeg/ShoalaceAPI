using Flunt.Notifications;
using Flunt.Validations;
using Shoalace.Domain.Validations;

namespace Shoalace.Domain.Commands.Usuario
{
    public class AtualizarVistoCommand : Command
    {
        public long Id { get; set; }
        public override void Validate() =>
            AddNotifications(new Contract<Notification>[]
            {
                UsuarioValidation.ValidateId(Id),
            });
    }
}
