using Flunt.Notifications;
using Flunt.Validations;
using Shoalace.Domain.Enums;
using Shoalace.Domain.Validations;

namespace Shoalace.Domain.Commands.Evento
{
    public class EditarMembroEventoCommand : NovoMembroEventoCommand
    {
        public long Id { get; set; }

        public override void Validate() =>
            AddNotifications(new Contract<Notification>[]
            {
                MembroEventoValidation.ValidateId(Id),
                MembroEventoValidation.ValidateUsuarioId(UsuarioId),
                MembroEventoValidation.ValidateEventoId(EventoId)
            });
    }
}
