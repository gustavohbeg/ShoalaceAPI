using Shoalace.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoalace.Domain.Responses
{
    public class MembroEventoResponse
    {
        public MembroEventoResponse(long id, long eventoId, long usuarioId, EComparecer comparecer, bool admin, UsuarioResponse usuario)
        {
            Id = id;
            EventoId = eventoId;
            UsuarioId = usuarioId;
            Comparecer = comparecer;
            Admin = admin;
            Usuario = usuario;
        }

        public long Id { get; set; }
        public long EventoId { get; set; }
        public long UsuarioId { get; set; }
        public EComparecer Comparecer { get; set; }
        public bool Admin { get; set; }
        public UsuarioResponse Usuario { get; set; }
    }
}
