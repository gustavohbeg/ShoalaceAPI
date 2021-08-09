using System.Collections.Generic;

namespace Shoalace.Domain.Commands.Evento
{
    public class InserirListaMembroEventoCommand : Command
    {
        public List<InserirMembroEventoCommand> MembrosEvento { get; set; }

        public override void Validate()
        {
            foreach (InserirMembroEventoCommand membro in MembrosEvento)
            {
                membro.Validate();
                AddNotifications(membro);
            }
        }
    }
}
