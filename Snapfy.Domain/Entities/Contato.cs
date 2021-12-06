using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shoalace.Domain.Entities
{
    public class Contato : Base
    {
        public Contato(long usuarioId, long? usuarioContatoId, string nome, string numero)
        {
            UsuarioId = usuarioId;
            UsuarioContatoId = usuarioContatoId;
            Nome = nome;
            Numero = numero;
        }

        public long UsuarioId { get; private set; }
        [JsonIgnore]
        public Usuario Usuario { get; private set; }
        public long? UsuarioContatoId { get; private set; }
        [JsonIgnore]
        public Usuario UsuarioContato { get; private set; }
        public string Nome { get; private set; }
        public string Numero { get; private set; }
    }
}
