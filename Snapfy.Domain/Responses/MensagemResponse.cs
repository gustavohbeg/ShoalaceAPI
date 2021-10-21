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
        public MensagemResponse(long id, string texto, long usuarioId, long? usuarioDestinoId, long? grupoId, string audio, string foto, EStatusMensagem status, DateTime cadastro)
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

        public long Id { get; private set; }
        public string Texto { get; private set; }
        public long UsuarioId { get; private set; }
        public long? UsuarioDestinoId { get; private set; }
        public long? GrupoId { get; private set; }
        public string Audio { get; private set; }
        public string Foto { get; private set; }
        public EStatusMensagem Status { get; private set; }
        public DateTime Cadastro { get; private set; }
    }
}
