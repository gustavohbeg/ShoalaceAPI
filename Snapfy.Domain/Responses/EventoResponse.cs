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
        public EventoResponse(long id, string titulo, string descricao, string cidade, string local, double valor, double? latitude, double? longitude, DateTime data, DateTime? hora, ETipoEvento tipo, long? grupoId, string foto, ECategoria categoria, List<MembroEventoResponse> membrosEvento)
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

        public long Id { get; private set; }
        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public string Cidade { get; private set; }
        public string Local { get; private set; }
        public double Valor { get; private set; }
        public double? Latitude { get; private set; }
        public double? Longitude { get; private set; }
        public DateTime Data { get; private set; }
        public DateTime? Hora { get; private set; }
        public ETipoEvento Tipo { get; private set; }
        public long? GrupoId { get; private set; }
        public string Foto { get; private set; }
        public ECategoria Categoria { get; private set; }
        public List<MembroEventoResponse> MembrosEvento { get; private set; }
    }
}
