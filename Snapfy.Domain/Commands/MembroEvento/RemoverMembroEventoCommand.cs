using Flunt.Notifications;
using Flunt.Validations;
using Shoalace.Domain.Validations;

namespace Shoalace.Domain.Commands
{
    public class RemoverMembroEventoCommand : Command
    {
        public long Id { get; set; }

        public override void Validate() =>
            AddNotifications(new Contract<Notification>[]
            {
                MembroEventoValidation.ValidateId(Id),
            });
    }
}
