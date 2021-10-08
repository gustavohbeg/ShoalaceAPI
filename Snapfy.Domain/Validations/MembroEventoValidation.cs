using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoalace.Domain.Validations
{
    public static class MembroEventoValidation
    {
        public static Contract<Notification> ValidateId(long id) =>
            new Contract<Notification>().Requires().AreNotEquals(id, 0, "MembroEvento.Id", "Id é obrigatório.");

        public static Contract<Notification> ValidateUsuarioId(long usuarioId) =>
            new Contract<Notification>().Requires().AreNotEquals(usuarioId, 0, "MembroEvento.UsuarioId", "Usuário é obrigatório.");

        public static Contract<Notification> ValidateEventoId(long eventoId) =>
            new Contract<Notification>().Requires().AreNotEquals(eventoId, 0, "MembroEvento.EventoId", "Evento é obrigatório.");
    }
}
