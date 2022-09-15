using Flunt.Notifications;
using Flunt.Validations;

namespace Shoalace.Domain.Validations
{
    public static class ContatoValidation
    {
        public const int NUMERO_MAXLENGTH = 11;

        public static Contract<Notification> ValidateUsuarioId(long usuarioId) =>
            new Contract<Notification>().Requires().AreNotEquals(usuarioId, 0, "Contato.UsuarioId", "Usuario é obrigatório.");

        public static Contract<Notification> ValidateNome(string nome) =>
            new Contract<Notification>().Requires().IsNotNullOrEmpty(nome, "Contato.Nome", "Nome é obrigatório.");

        public static Contract<Notification> ValidateNumero(string numero) =>
            new Contract<Notification>().Requires().IsNotNullOrEmpty(numero, "Contato.Numero", "Numero é obrigatório.").AreEquals(numero, NUMERO_MAXLENGTH, "Cpf", $"CPF deve conter {NUMERO_MAXLENGTH} caracteres.");
    }
}
