using Shoalace.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shoalace.Domain.Entities
{
    public class Evento : Base
    {
        private readonly List<MembroEvento> _membrosEvento;
        public Evento(string titulo, string descricao, string local, double valor, double? latitude, double? longitude, DateTime data, DateTime? hora, ETipo tipo, long? grupoId, string foto, ECategoria categoria) : base()
        {
            Titulo = titulo;
            Descricao = descricao;
            Cidade = "";
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
            _membrosEvento = new List<MembroEvento>();
        }

        public void PreencherEvento(string titulo, string descricao, string local, double valor, double? latitude, double? longitude, DateTime data, DateTime? hora, ETipo tipo, long? grupoId, string foto, ECategoria categoria)
        {
            Alterado = DateTime.Now;
            Titulo = titulo;
            Descricao = descricao;
            Cidade = "";
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
        }

        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public string Cidade { get; private set; }
        public string Local { get; private set; }
        public double Valor { get; private set; }
        public double? Latitude { get; private set; }
        public double? Longitude { get; private set; }
        public DateTime Data { get; private set; }
        public DateTime? Hora { get; private set; }
        public ETipo Tipo { get; private set; }
        public long? GrupoId { get; private set; }
        public string Foto { get; private set; }
        public ECategoria Categoria { get; private set; }
        public IReadOnlyCollection<MembroEvento> MembrosEvento { get => _membrosEvento; }

        public string DiaSemana { get => Data.ToString("dddd"); }
        public bool DiaInteiro { get => Hora == null; }
        public int Confirmados { get => _membrosEvento.Where(m => m.Comparecer == EComparecer.Sim).Count(); }

        public void AdicionarMembroEvento(MembroEvento membroEvento)
        {
            _membrosEvento.Add(membroEvento);
        }

        public void FazerCheckIn(MembroEvento membroEvento)
        {
            _membrosEvento.FirstOrDefault(a => a.Id == membroEvento.Id || a.UsuarioId == membroEvento.UsuarioId)?.PreencherMembroEvento(membroEvento.UsuarioId, membroEvento.EventoId, membroEvento.Comparecer, membroEvento.Admin);
        }

        public void RemoverMembro(MembroEvento membroEvento)
        {
            _membrosEvento.Remove(membroEvento);
        }
    }
}
