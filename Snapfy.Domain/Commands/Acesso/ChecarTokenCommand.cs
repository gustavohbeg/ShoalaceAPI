using Flunt.Notifications;
using Flunt.Validations;
using Shoalace.Domain.Validations;

namespace Shoalace.Domain.Commands.Acesso
{
    public class ChecarTokenCommand : Command
    {
        public long UsuarioId { get; set; }
        public string Token { get; set; }
        public override void Validate() =>
            AddNotifications(new Contract<Notification>[]
            {
                AcessoValidation.ValidateUsuarioId(UsuarioId),
                AcessoValidation.ValidateToken(Token),
            });
    }
}
