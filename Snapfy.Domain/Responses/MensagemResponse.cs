using Shoalace.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoalace.Domain.Responses
{
    public class MensagemResponse
    {
        public long Id { get; set; }
        public string Texto { get; set; }
        public long UsuarioId { get; set; }
        public long? UsuarioDestinoId { get; set; }
        public long? GrupoId { get; set; }
        public string Audio { get; set; }
        public string Foto { get; set; }
        public EStatus Status { get; set; }
    }
}
