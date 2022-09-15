using Flunt.Notifications;
using Flunt.Validations;
using Shoalace.Domain.Enums;
using Shoalace.Domain.Validations;
using System;
using System.Collections.Generic;

namespace Shoalace.Domain.Entities
{
    public class Usuario : Base
    {
        private List<Contato> _contatos;
        private List<Contato> _eContato;
        public Usuario(string numero, DateTime aniversario, ESexo sexo, string foto, string nome, string bio, DateTime visto, double? latitude, double? longitude, string token) : base()
        {
            _contatos = new();
            _eContato = new();
            PreencherUsuario(numero, aniversario, sexo, foto, nome, bio, visto, latitude, longitude, token);
        }

        public void PreencherUsuario(string numero, DateTime aniversario, ESexo sexo, string foto, string nome, string bio, DateTime visto, double? latitude, double? longitude, string token)
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
            Validate();
        }

        public void Validate() =>
            AddNotifications(new Contract<Notification>[]
            {
                UsuarioValidation.ValidateNome(Nome),
                UsuarioValidation.ValidateToken(Token)
            });

        public string Numero { get; private set; }
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

        public IReadOnlyCollection<Contato> Contatos { get => _contatos; }
        public IReadOnlyCollection<Contato> EContato { get => _eContato; }


    public void AtualizarVisto() => Visto = DateTime.Now;
}
}
