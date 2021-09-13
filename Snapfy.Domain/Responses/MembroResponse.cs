using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoalace.Domain.Responses
{
    public class MembroResponse
    {
        public long Id { get; set; }
        public long UsuarioId { get; set; }
        public bool Admin { get; set; }
    }
}
