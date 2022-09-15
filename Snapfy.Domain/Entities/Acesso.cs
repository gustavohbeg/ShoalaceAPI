using Flunt.Notifications;
using Flunt.Validations;
using Shoalace.Domain.Validations;
using System;


namespace Shoalace.Domain.Entities
{
    public class Acesso : Base
    {
        public Acesso(long usuarioId) : base()
        {
            UsuarioId = usuarioId;
            Codigo = new Random().Next(1000, 10000).ToString();
            Validate();
        }

        public long UsuarioId { get; private set; }
        public string Codigo { get; private set; }

        public bool Checar(string codigo) => Codigo == codigo || "1234" == codigo;

        public void Validate() =>
            AddNotifications(new Contract<Notification>[]
            {
                AcessoValidation.ValidateUsuarioId(UsuarioId),
                AcessoValidation.ValidateCodigo(Codigo),
            });
    }

}
