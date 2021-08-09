using System.Collections.Generic;

namespace Shoalace.Domain.Commands.Evento
{
    public class NovoListaEventoCommand : Command
    {
        public List<NovoEventoCommand> Eventos { get; set; }

        public override void Validate()
        {
            foreach (NovoEventoCommand evento in Eventos)
            {
                evento.Validate();
                AddNotifications(evento);
            }
        }
    }
}
