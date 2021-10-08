using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoalace.Domain.Validations
{
    public static class EventoValidation
    {
        public static Contract<Notification> ValidateId(long id) =>
            new Contract<Notification>().Requires().AreNotEquals(id, 0, "Evento.Id", "Id é obrigatório.");

        public static Contract<Notification> ValidateTitulo(string titulo) =>
            new Contract<Notification>().Requires().IsNotNullOrEmpty(titulo, "Evento.Titulo", "Título é obrigatório.");
    }
}
