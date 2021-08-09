using System.Collections.Generic;

namespace Shoalace.Domain.Commands.Mensagem
{
    public class NovoListaMensagemCommand : Command
    {
        public List<NovoMensagemCommand> Mensagens { get; set; }

        public override void Validate()
        {
            foreach (NovoMensagemCommand mensagem in Mensagens)
            {
                mensagem.Validate();
                AddNotifications(mensagem);
            }
        }
    }
}
