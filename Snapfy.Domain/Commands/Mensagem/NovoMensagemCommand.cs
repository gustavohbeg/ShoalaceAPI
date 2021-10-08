using Flunt.Notifications;
using Flunt.Validations;
using Shoalace.Domain.Enums;
using Shoalace.Domain.Validations;

namespace Shoalace.Domain.Commands.Mensagem
{
    public class NovoMensagemCommand : Command
    {
        public string Texto { get; set; }
        public long UsuarioId { get; set; }
        public long? UsuarioDestinoId { get; set; }
        public long? GrupoId { get; set; }
        public string Audio { get; set; }
        public string Foto { get; set; }
        public EStatusMensagem Status { get; set; }

        public override void Validate() =>
           AddNotifications(new Contract<Notification>[]
           {
                MensagemValidation.ValidateUsuarioId(UsuarioId),
                MensagemValidation.ValidateDestino(UsuarioDestinoId, GrupoId)
           });
    }
}
