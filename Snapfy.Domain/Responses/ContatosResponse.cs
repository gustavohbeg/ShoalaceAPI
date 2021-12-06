using Shoalace.Domain.Entities;
using Shoalace.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoalace.Domain.Responses
{
    public class ContatosResponse
    {
        public long? Id { get; set; }
        public string Numero { get; set; }
        public string Nome { get; set; }
        public string Foto { get; set; }
        public DateTime? Cadastro { get; set; }
        public DateTime? Aniversario { get; set; }
        public string Bio { get; set; }
        public DateTime? Visto { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public int Existente { get; set; }
    }
}
