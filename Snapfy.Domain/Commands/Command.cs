using Flunt.Notifications;
using Flunt.Validations;

namespace Shoalace.Domain.Commands
{
    public abstract class Command : Notifiable, IValidatable
    {
        public abstract void Validate();
    }
}
