using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoalace.Domain.Validations
{
    public static class GrupoValidation
    {
        public static Contract<Notification> ValidateId(long id) =>
            new Contract<Notification>().Requires().AreNotEquals(id, 0, "Grupo.Id", "Id do grupo é obrigatório.");

        public static Contract<Notification> ValidateNome(string titulo) =>
            new Contract<Notification>().Requires().IsNotNullOrEmpty(titulo, "Grupo.Nome", "Nome do grupo é obrigatório.");
    }
}
