using Flunt.Notifications;
using Flunt.Validations;
using Shoalace.Domain.Enums;
using Shoalace.Domain.Validations;
using System;

namespace Shoalace.Domain.Commands.Usuario
{
    public class EditarUsuarioCommand : NovoUsuarioCommand
    {
        public long Id { get; set; }

        public override void Validate() =>
            AddNotifications(new Contract<Notification>[]
            {
                UsuarioValidation.ValidateId(Id),
                UsuarioValidation.ValidateNome(Nome),
                UsuarioValidation.ValidateToken(Token)
            });
    }
}
