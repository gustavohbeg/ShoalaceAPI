using System.Collections.Generic;

namespace Shoalace.Domain.Commands.Evento
{
    public class NovoListaMembroEventoCommand : Command
    {
        public List<NovoMembroEventoCommand> MembrosEvento { get; set; }

        public override void Validate()
        {
            foreach (NovoMembroEventoCommand membro in MembrosEvento)
            {
                membro.Validate();
                AddNotifications(membro);
            }
        }
    }
}
