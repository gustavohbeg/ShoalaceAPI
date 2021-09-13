using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoalace.Domain.Responses
{
    public class ContatosChat
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Foto { get; set; }
        public bool IsGrupo { get; set; }
        public DateTime Cadastro { get; set; }
        public List<MensagemResponse> Mensagens { get; set; }
        public List<MembroResponse> Membros { get; set; }
    }
}
