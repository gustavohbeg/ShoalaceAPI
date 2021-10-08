using Flunt.Notifications;
using Flunt.Validations;
using Shoalace.Domain.Enums;
using Shoalace.Domain.Validations;
using System.Collections.Generic;

namespace Shoalace.Domain.Commands.Evento
{
    public class NovoMembroEventoCommand : Command
    {
        public long UsuarioId { get; set; }
        public long EventoId { get; set; }
        public bool Admin { get; private set; }
        public EComparecer Comparecer { get; set; }

        public override void Validate() =>
            AddNotifications(new Contract<Notification>[]
            {
                MembroEventoValidation.ValidateUsuarioId(UsuarioId),
                MembroEventoValidation.ValidateEventoId(EventoId),
            });
    }
}
