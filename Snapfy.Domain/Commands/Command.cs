using Flunt.Notifications;
using Flunt.Validations;

namespace Shoalace.Domain.Commands
{
    public abstract class Command : Notifiable<Notification>
    {
        public bool Valid { get => IsValid; }
        public bool Invalid { get => !IsValid; }
        public abstract void Validate();
    }
}
