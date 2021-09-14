using Shoalace.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoalace.Domain.Responses
{
    public class UsuarioResponse
    {
        public long Id { get; set; }
        public long Numero { get; set; }
        public DateTime Aniversario { get; set; }
        public ESexo Sexo { get; set; }
        public string Foto { get; set; }
        public string Nome { get; set; }
        public string Bio { get; set; }
        public DateTime Visto { get; set; }
        public bool Online { get; set; }
    }
}
