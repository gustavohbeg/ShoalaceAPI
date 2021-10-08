using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoalace.Domain.Validations
{
    public static class MembroValidation
    {
        public static Contract<Notification> ValidateId(long id) =>
            new Contract<Notification>().Requires().AreNotEquals(id, 0, "Membro.Id", "Id é obrigatório.");

        public static Contract<Notification> ValidateUsuarioId(long usuarioId) =>
            new Contract<Notification>().Requires().AreNotEquals(usuarioId, 0, "Membro.UsuarioId", "Usuário é obrigatório.");

        public static Contract<Notification> ValidateGrupoId(long grupoId) =>
            new Contract<Notification>().Requires().AreNotEquals(grupoId, 0, "Membro.GrupoId", "Grupo é obrigatório.");
    }
}
