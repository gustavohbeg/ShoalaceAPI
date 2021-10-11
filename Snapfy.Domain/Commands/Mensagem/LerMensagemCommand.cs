using Flunt.Notifications;
using Flunt.Validations;
using Shoalace.Domain.Enums;
using Shoalace.Domain.Validations;
using System.Collections.Generic;

namespace Shoalace.Domain.Commands.Mensagem
{
    public class LerMensagensCommand : Command
    {
        public List<EditarMensagemCommand> Mensagens { get; set; }

        public override void Validate()
        {
            foreach (EditarMensagemCommand mensagemCommand in Mensagens)
            {
                mensagemCommand.Validate();
                AddNotifications(mensagemCommand);
            }
            
        }
    }
}
