using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoalace.Domain.Validations
{
    public static class MensagemValidation
    {
        public static Contract<Notification> ValidateId(long id) =>
            new Contract<Notification>().Requires().AreNotEquals(id, 0, "Mensagem.Id", "Id da mensagem é obrigatório.");

        public static Contract<Notification> ValidateUsuarioId(long usuarioId) =>
            new Contract<Notification>().Requires().AreNotEquals(usuarioId, 0, "Mensagem.UsuarioId", "Usuario de origem é obrigatório.");

        public static Contract<Notification> ValidateDestino(long? usuarioId, long? grupoId) =>
            new Contract<Notification>().Requires().AreNotEquals(usuarioId != null ? usuarioId.Value : 0 + grupoId != null ? grupoId.Value : 0, 0, "Mensagem.Destino", "Destino da mensagem é obrigatório.");
    }
}
