using Flunt.Validations;

namespace Shoalace.Domain.Commands.Acesso
{
    public class ChecarTokenCommand : Command
    {
        public long UsuarioId { get; set; }
        public string Token { get; set; }
        public override void Validate()
        {
            AddNotifications(new Contract()
                .AreNotEquals(UsuarioId, 0, "Acesso.UsuarioId", "Usuario é obrigatório.")
                .IsNotNullOrEmpty(Token, "Acesso.Token", "Token é obrigatório.")
                );
        }
    }
}
