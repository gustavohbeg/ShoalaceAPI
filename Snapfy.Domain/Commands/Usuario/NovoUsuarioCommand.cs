using Flunt.Notifications;
using Flunt.Validations;
using Shoalace.Domain.Enums;
using Shoalace.Domain.Validations;
using System;

namespace Shoalace.Domain.Commands.Usuario
{
    public class NovoUsuarioCommand : Command
    {
        public string Numero { get; set; }
        public DateTime Aniversario { get; set; }
        public ESexo Sexo { get; set; }
        public string Foto { get; set; }
        public string Nome { get; set; }
        public string Bio { get; set; }
        public DateTime Visto { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Token { get; set; }

        public override void Validate() =>
            AddNotifications(new Contract<Notification>[]
            {
                UsuarioValidation.ValidateNome(Nome),
                UsuarioValidation.ValidateToken(Token)
            });
    }
}
