using System.Collections.Generic;

namespace Shoalace.Domain.Commands.Grupo
{
    public class NovoListaMembroCommand : Command
    {
        public List<NovoMembroCommand> Membros { get; set; }

        public override void Validate()
        {
            foreach (NovoMembroCommand membro in Membros)
            {
                membro.Validate();
                AddNotifications(membro);
            }
        }
    }
}
