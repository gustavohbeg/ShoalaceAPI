using Flunt.Notifications;
using System;

namespace Shoalace.Domain.Entities
{
    public abstract class Base : Notifiable
    {
        public Base()
        {
            Id = 0;
            Cadastro = DateTime.Now;
            Alterado = DateTime.Now;
        }

        public long Id { get; protected set; }
        public DateTime Cadastro { get; protected set; }
        public DateTime Alterado { get; protected set; }
        public bool Pertence(long id) => id == Id;
    }
}
