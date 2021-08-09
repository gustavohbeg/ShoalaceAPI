using Flunt.Validations;
using Shoalace.Domain.Enums;

namespace Shoalace.Domain.Commands.Evento
{
    public class EditarMembroEventoCommand : Command
    {
        public long Id { get; set; }
        public long UsuarioId { get; set; }
        public long EventoId { get; set; }
        public bool Admin { get; set; }
        public EComparecer Comparecer { get; set; }

        public override void Validate()
        {
            AddNotifications(new Contract()
                .AreNotEquals(Id, 0, "MembroEvento.Id", "Membro é obrigatório.")
                .AreNotEquals(UsuarioId, 0, "MembroEvento.UsuarioId", "Usuario é obrigatório.")
                .AreNotEquals(EventoId, 0, "MembroEvento.EventoId", "Evento é obrigatório.")
                );
        }
    }
}
