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
        public UsuarioResponse(long id, long numero, DateTime aniversario, ESexo sexo, string foto, string nome, string bio, DateTime visto, bool online)
        {
            Id = id;
            Numero = numero;
            Aniversario = aniversario;
            Sexo = sexo;
            Foto = foto;
            Nome = nome;
            Bio = bio;
            Visto = visto;
            Online = online;
        }

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
