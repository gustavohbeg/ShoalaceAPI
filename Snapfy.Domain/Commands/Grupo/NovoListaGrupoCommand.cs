using System.Collections.Generic;

namespace Shoalace.Domain.Commands.Grupo
{
    public class NovoListaGrupoCommand : Command
    {
        public List<NovoGrupoCommand> Grupos { get; set; }

        public override void Validate()
        {
            foreach (NovoGrupoCommand grupo in Grupos)
            {
                grupo.Validate();
                AddNotifications(grupo);
            }
        }
    }
}
