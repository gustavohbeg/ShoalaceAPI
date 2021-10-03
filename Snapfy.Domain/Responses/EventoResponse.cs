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
        public EventoResponse(long id, string titulo, string descricao, string cidade, string local, double valor, double? latitude, double? longitude, DateTime data, DateTime? hora, ETipo tipo, long? grupoId, string foto, ECategoria categoria, List<MembroEventoResponse> membrosEvento)
        {
            Id = id;
            Titulo = titulo;
            Descricao = descricao;
            Cidade = cidade;
            Local = local;
            Valor = valor;
            Latitude = latitude;
            Longitude = longitude;
            Data = data;
            Hora = hora;
            Tipo = tipo;
            GrupoId = grupoId;
            Foto = foto;
            Categoria = categoria;
            MembrosEvento = membrosEvento;
        }

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
        public ETipo Tipo { get; set; }
        public long? GrupoId { get; set; }
        public string Foto { get; set; }
        public ECategoria Categoria { get; set; }
        public List<MembroEventoResponse> MembrosEvento { get; set; }
    }
}
