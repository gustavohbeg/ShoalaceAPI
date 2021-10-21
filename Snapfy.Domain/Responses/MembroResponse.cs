using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoalace.Domain.Responses
{
    public class MembroResponse
    {
        public MembroResponse(long id, long usuarioId, bool admin, UsuarioResponse usuario)
        {
            Id = id;
            UsuarioId = usuarioId;
            Admin = admin;
            Usuario = usuario;
        }

        public long Id { get; private set; }
        public long UsuarioId { get; private set; }
        public bool Admin { get; private set; }
        public  UsuarioResponse Usuario { get; private set; }
    }
}
