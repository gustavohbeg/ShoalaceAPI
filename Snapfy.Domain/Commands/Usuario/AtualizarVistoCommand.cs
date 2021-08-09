using Flunt.Validations;

namespace Shoalace.Domain.Commands.Usuario
{
    public class AtualizarVistoCommand : Command
    {
        public long Id { get; set; }
        public override void Validate()
        {
            AddNotifications(new Contract()
                .AreNotEquals(Id, 0, "Usuario.Id", "Usuario é obrigatório.")
                );
        }
    }
}
