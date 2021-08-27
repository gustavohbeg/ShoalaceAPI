using Flunt.Validations;
using Shoalace.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoalace.Domain.Commands.Evento
{
    public class MembroEventoCommand : Command
    {
        public long UsuarioId { get; set; }
        public EComparecer Comparecer { get; set; }
        public bool Admin { get; set; }

        public override void Validate()
        {
            AddNotifications(new Contract()
                 .AreNotEquals(UsuarioId, 0, "Membro.UsuarioId", "Usuario é obrigatório.")
                 );
        }
    }
}
