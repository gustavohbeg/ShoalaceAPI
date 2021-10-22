using Shoalace.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoalace.Domain.Responses
{
    public class EventoResponse
    {
        public long Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Cidade { get; set; }
        public string Local { get; set; }
        public double Valor { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public DateTime Data { get; set; }
        public DateTime? Hora { get; set; }
        public ETipoEvento Tipo { get; set; }
        public long? GrupoId { get; set; }
        public string Foto { get; set; }
        public ECategoria Categoria { get; set; }
        public List<MembroEventoResponse> MembrosEvento { get; set; }
    }
}
