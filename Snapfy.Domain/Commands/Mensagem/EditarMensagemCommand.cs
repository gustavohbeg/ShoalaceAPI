using Flunt.Notifications;
using Flunt.Validations;
using Shoalace.Domain.Enums;
using Shoalace.Domain.Validations;

namespace Shoalace.Domain.Commands.Mensagem
{
    public class EditarMensagemCommand : NovoMensagemCommand
    {
        public long Id { get; set; }

        public override void Validate() =>
            AddNotifications(new Contract<Notification>[]
            {
                MensagemValidation.ValidateId(Id),
                MensagemValidation.ValidateUsuarioId(UsuarioId),
                MensagemValidation.ValidateDestino(UsuarioDestinoId, GrupoId)
            });
    }
}
