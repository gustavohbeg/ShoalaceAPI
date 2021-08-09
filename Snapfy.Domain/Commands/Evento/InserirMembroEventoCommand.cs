using Flunt.Validations;
using Shoalace.Domain.Enums;

namespace Shoalace.Domain.Commands.Evento
{
    public class InserirMembroEventoCommand : Command
    {
        public long UsuarioId { get; set; }
        public long EventoId { get; set; }
        public bool Admin { get; private set; }
        public EComparecer Comparecer { get; set; }

        public override void Validate()
        {
            AddNotifications(new Contract()
                 .AreNotEquals(UsuarioId, 0, "MembroEvento.UsuarioId", "Usuario é obrigatório.")
                 .AreNotEquals(EventoId, 0, "MembroEvento.EventoId", "Evento é obrigatório.")
                 );
        }
    }
}
