using Flunt.Notifications;
using Flunt.Validations;
using Shoalace.Domain.Entities;
using Shoalace.Domain.Validations;
using System.Collections.Generic;

namespace Shoalace.Domain.Commands.Grupo
{
    public class NovoGrupoCommand : Command
    {
        public string Nome { get; set; }
        public string Foto { get; set; }
        public List<MembroCommand> Membros { get; set; }

        public override void Validate() =>
            AddNotifications(new Contract<Notification>[]
            {
                GrupoValidation.ValidateNome(Nome),
            });
    }
}
