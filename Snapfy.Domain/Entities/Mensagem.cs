using Shoalace.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Shoalace.Domain.Entities
{
    public class Mensagem : Base
    {
        private readonly List<StatusMensagem> _statusMensagens;
        public Mensagem(string texto, long usuarioId, long? usuarioDestinoId, long? grupoId, string audio, string foto, EStatus status) : base()
        {
            Texto = texto;
            UsuarioId = usuarioId;
            UsuarioDestinoId = usuarioDestinoId;
            GrupoId = grupoId;
            Audio = audio;
            Foto = foto;
            Status = status;
            _statusMensagens = new List<StatusMensagem>();
        }

        public void PreencherMensagem(string texto, long usuarioId, long? usuarioDestinoId, long? grupoId, string audio, string foto, EStatus status)
        {
            Alterado = DateTime.Now;
            Texto = texto;
            UsuarioId = usuarioId;
            UsuarioDestinoId = usuarioDestinoId;
            GrupoId = grupoId;
            Audio = audio;
            Foto = foto;
            Status = status;
        }

        public string Texto { get; private set; }
        public long UsuarioId { get; private set; }
        public Usuario Usuario { get; private set; }
        public long? UsuarioDestinoId { get; private set; }
        public Usuario UsuarioDestino { get; private set; }
        public long? GrupoId { get; private set; }
        public Grupo Grupo { get; private set; }
        public string Audio { get; private set; }
        public string Foto { get; private set; }
        public EStatus Status { get; private set; }

        public IReadOnlyCollection<StatusMensagem> StatusMensagens { get => _statusMensagens; }
    }
}
