using Flunt.Validations;

namespace Shoalace.Domain.Commands.Acesso
{
    public class ChecarAcessoCommand : Command
    {
        public long UsuarioId { get; set; }
        public string Codigo { get; set; }
        public override void Validate()
        {
            AddNotifications(new Contract()
                .AreNotEquals(UsuarioId, 0, "Acesso.UsuarioId", "Usuario é obrigatório.")
                .IsNotNullOrEmpty(Codigo, "Acesso.Codigo", "Codigo é obrigatório.")
                );
        }
    }
}
