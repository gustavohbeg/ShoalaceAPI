using System.Collections.Generic;

namespace Shoalace.Domain.Commands.Grupo
{
    public class InserirListaMembroCommand : Command
    {
        public List<InserirMembroCommand> Membros { get; set; }

        public override void Validate()
        {
            foreach (InserirMembroCommand membro in Membros)
            {
                membro.Validate();
                AddNotifications(membro);
            }
        }
    }
}
