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
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Foto { get; set; }
        public bool IsGrupo { get; set; }
        public string Texto { get; set; }
        public EStatusMensagem Status { get; set; }
        public DateTime Cadastro { get; set; }
        public int NaoLidas { get; set; }
        public long UsuarioId { get; set; }
        public MensagemResponse UltimaMensagem { get; set; }
    }
}
