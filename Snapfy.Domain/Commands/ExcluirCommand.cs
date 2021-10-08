using Flunt.Validations;

namespace Shoalace.Domain.Commands
{
    public class ExcluirCommand : Command
    {
        public long Id { get; set; }

        public override void Validate() =>
            AddNotifications(new Contract<Command>()
                .AreNotEquals(Id, 0, "Id", "ID é obrigatório.")
            );
    }
}
