using Flunt.Validations;
using Shoalace.Domain.Enums;
using System;

namespace Shoalace.Domain.Commands.Usuario
{
    public class NovoUsuarioCommand : Command
    {
        public long Numero { get; set; }
        public DateTime Aniversario { get; set; }
        public ESexo Sexo { get; set; }
        public string Foto { get; set; }
        public string Nome { get; set; }
        public string Bio { get; set; }
        public DateTime Visto { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Token { get; set; }

        public override void Validate()
        {
            AddNotifications(new Contract()
              .IsNotNull(Aniversario, "Usuario.Aniversario", "Aniversário é obrigatório")
              .IsNotNullOrEmpty(Nome, "Usuario.Nome", "Nome é obrigatório.")
              .IsNotNullOrEmpty(Nome, "Usuario.Token", "Token é obrigatório.")
              );
        }
    }
}
