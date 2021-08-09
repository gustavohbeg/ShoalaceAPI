using Flunt.Validations;

namespace Shoalace.Domain.Commands
{
    public class RemoverMembroEventoCommand : Command
    {
        public long Id { get; set; }

        public override void Validate()
        {
            AddNotifications(new Contract()
                .AreNotEquals(Id, 0, "Id", "ID é obrigatório.")
            );
        }
    }
}
