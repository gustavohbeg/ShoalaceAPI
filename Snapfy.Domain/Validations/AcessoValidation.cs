using Flunt.Notifications;
using Flunt.Validations;

namespace Shoalace.Domain.Validations
{
    public static class AcessoValidation
    {
        public const int CODIGO_MAXLENGTH = 4;

        public static Contract<Notification> ValidateId(long id) =>
            new Contract<Notification>().Requires().AreNotEquals(id, 0, "Acesso.Id", "Id é obrigatório.");

        public static Contract<Notification> ValidateUsuarioId(long usuarioId) =>
            new Contract<Notification>().Requires().AreNotEquals(usuarioId, 0, "Acesso.UsuarioId", "Usuario é obrigatório.");

        public static Contract<Notification> ValidateCodigo(string codigo) =>
            new Contract<Notification>().Requires().IsNotNullOrEmpty(codigo, "Acesso.Codigo", "Codigo é obrigatório.");

        public static Contract<Notification> ValidateToken(string token) =>
            new Contract<Notification>().Requires().IsNotNullOrEmpty(token, "Acesso.Token", "Token é obrigatório.");
    }
}
