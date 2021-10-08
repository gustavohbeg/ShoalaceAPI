using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoalace.Domain.Validations
{
    public static class UsuarioValidation
    {
        public static Contract<Notification> ValidateId(long id) =>
            new Contract<Notification>().Requires().AreNotEquals(id, 0, "Usuario.Id", "Id é obrigatório.");

        public static Contract<Notification> ValidateNome(string nome) =>
            new Contract<Notification>().Requires().IsNotNullOrEmpty(nome, "Usuario.Nome", "Nome é obrigatório.");

        public static Contract<Notification> ValidateToken(string token) =>
            new Contract<Notification>().Requires().IsNotNullOrEmpty(token, "Usuario.Token", "Token é obrigatório.");
    }
}
