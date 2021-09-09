using Shoalace.Domain.Enums;
using System;

namespace Shoalace.Domain.Entities
{
    public class Usuario : Base
    {
        public Usuario(long numero, DateTime aniversario, ESexo sexo, string foto, string nome, string bio, DateTime visto, double? latitude, double? longitude, string token) : base()
        {
            Numero = numero;
            Aniversario = aniversario;
            Sexo = sexo;
            Foto = foto;
            Nome = nome;
            Bio = bio;
            Visto = visto;
            Latitude = latitude;
            Longitude = longitude;
            Token = token;
        }

        public void PreencherUsuario(long numero, DateTime aniversario, ESexo sexo, string foto, string nome, string bio, DateTime visto, double? latitude, double? longitude, string token)
        {
            Numero = numero;
            Alterado = DateTime.Now;
            Aniversario = aniversario;
            Sexo = sexo;
            Foto = foto;
            Nome = nome;
            Bio = bio;
            Visto = visto;
            Latitude = latitude;
            Longitude = longitude;
            Token = token;
        }

        public long Numero { get; private set; }
        public DateTime Aniversario { get; private set; }
        public ESexo Sexo { get; private set; }
        public string Foto { get; private set; }
        public string Nome { get; private set; }
        public string Bio { get; private set; }
        public DateTime Visto { get; private set; }
        public double? Latitude { get; private set; }
        public double? Longitude { get; private set; }
        public string Token { get; private set; }

        public bool Online { get => Visto >= DateTime.Now.AddMinutes(-1); }

        public void AtualizarVisto() => Visto = DateTime.Now;
    }
}
