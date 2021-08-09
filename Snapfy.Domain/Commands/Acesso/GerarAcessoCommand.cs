using Flunt.Validations;

namespace Shoalace.Domain.Commands.Acesso
{
    public class GerarAcessoCommand : Command
    {
        public long UsuarioId { get; set; }
        public override void Validate()
        {
            AddNotifications(new Contract()
                .AreNotEquals(UsuarioId, 0, "Acesso.UsuarioId", "Usuario é obrigatório.")
                );
        }
    }
}
