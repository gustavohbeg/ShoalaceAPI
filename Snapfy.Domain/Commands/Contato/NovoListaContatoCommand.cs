using Flunt.Notifications;
using Flunt.Validations;
using Shoalace.Domain.Enums;
using Shoalace.Domain.Validations;
using System;
using System.Collections.Generic;

namespace Shoalace.Domain.Commands.Usuario
{
    public class NovoListaContatoCommand : Command
    {
        public long Id { get; set; }
        public List<long> Contatos { get; set; }

        public override void Validate() =>
            AddNotifications(new Contract<Notification>[]
            {
                UsuarioValidation.ValidateNome("asd")
            });
    }
}
