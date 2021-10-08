using Flunt.Notifications;
using Flunt.Validations;
using Shoalace.Domain.Validations;

namespace Shoalace.Domain.Commands.Acesso
{
    public class GerarAcessoCommand : Command
    {
        public long UsuarioId { get; set; }
        public override void Validate() =>
            AddNotifications(new Contract<Notification>[]
            {
                AcessoValidation.ValidateUsuarioId(UsuarioId),
            });
    }
}
