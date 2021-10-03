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
        public MensagemResponse(long id, string texto, long usuarioId, long? usuarioDestinoId, long? grupoId, string audio, string foto, EStatus status, DateTime cadastro)
        {
            Id = id;
            Texto = texto;
            UsuarioId = usuarioId;
            UsuarioDestinoId = usuarioDestinoId;
            GrupoId = grupoId;
            Audio = audio;
            Foto = foto;
            Status = status;
            Cadastro = cadastro;
        }

        public long Id { get; set; }
        public string Texto { get; set; }
        public long UsuarioId { get; set; }
        public long? UsuarioDestinoId { get; set; }
        public long? GrupoId { get; set; }
        public string Audio { get; set; }
        public string Foto { get; set; }
        public EStatus Status { get; set; }
        public DateTime Cadastro { get; set; }
    }
}
