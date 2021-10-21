using Shoalace.Domain.Entities;
using Shoalace.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoalace.Domain.Responses
{
    public class ContatosHome
    {
        public ContatosHome(long id, string nome, string foto, bool isGrupo, string texto, EStatusMensagem status, DateTime cadastro, int naoLidas, long usuarioId, MensagemResponse ultimaMensagem)
        {
            Id = id;
            Nome = nome;
            Foto = foto;
            IsGrupo = isGrupo;
            Texto = texto;
            Status = status;
            Cadastro = cadastro;
            NaoLidas = naoLidas;
            UsuarioId = usuarioId;
            UltimaMensagem = ultimaMensagem;
        }

        public long Id { get; private set; }
        public string Nome { get; private set; }
        public string Foto { get; private set; }
        public bool IsGrupo { get; private set; }
        public string Texto { get; private set; }
        public EStatusMensagem Status { get; private set; }
        public DateTime Cadastro { get; private set; }
        public int NaoLidas { get; private set; }
        public long UsuarioId { get; private set; }
        public MensagemResponse UltimaMensagem { get; private set; }
    }
}
