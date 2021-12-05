using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoalace.Domain.Entities
{
    public class Contato : Base
    {
        public Contato(long usuarioId, long usuarioContatoId)
        {
            UsuarioId = usuarioId;
            UsuarioContatoId = usuarioContatoId;
        }

        public long UsuarioId { get; private set; }
        public Usuario Usuario { get; private set; }
        public long UsuarioContatoId { get; private set; }
        public Usuario UsuarioContato { get; private set; }
    }
}
