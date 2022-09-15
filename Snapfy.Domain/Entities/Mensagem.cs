using Flunt.Notifications;
using Flunt.Validations;
using Shoalace.Domain.Enums;
using Shoalace.Domain.Validations;
using System;
using System.Collections.Generic;

namespace Shoalace.Domain.Entities
{
    public class Mensagem : Base
    {
        private readonly List<StatusMensagem> _statusMensagens;
        public Mensagem(string texto, long usuarioId, long? usuarioDestinoId, long? grupoId, string audio, string foto, EStatusMensagem status) : base()
        {
            _statusMensagens = new();
            PreencherMensagem(texto, usuarioId, usuarioDestinoId, grupoId, audio, foto, status);
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
        public EStatusMensagem Status { get; private set; }

        public IReadOnlyCollection<StatusMensagem> StatusMensagens { get => _statusMensagens; }
        public string MensagemDinamica { get => Texto ?? Audio ?? Foto; }


        public void PreencherMensagem(string texto, long usuarioId, long? usuarioDestinoId, long? grupoId, string audio, string foto, EStatusMensagem status)
        {
            Alterado = DateTime.Now;
            Texto = texto;
            UsuarioId = usuarioId;
            UsuarioDestinoId = usuarioDestinoId;
            GrupoId = grupoId;
            Audio = audio;
            Foto = foto;
            Status = status;
            Validate();
        }

        public void Validate() =>
            AddNotifications(new Contract<Notification>[]
            {
                MensagemValidation.ValidateUsuarioId(UsuarioId),
                MensagemValidation.ValidateDestino(UsuarioDestinoId, GrupoId)
            });

        public void Ler() => Status = EStatusMensagem.Lida;
    }
}
