using System;

namespace Shoalace.Domain.Entities
{
    public class Acesso : Base
    {
        public Acesso(long usuarioId) : base()
        {
            UsuarioId = usuarioId;
            Codigo = new Random().ToString();
        }

        public long UsuarioId { get; private set; }
        public string Codigo { get; private set; }
        
        public bool Checar(string codigo) => Codigo == codigo;
    }
}
