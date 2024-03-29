﻿using Flunt.Notifications;
using Flunt.Validations;
using Shoalace.Domain.Enums;
using Shoalace.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shoalace.Domain.Entities
{
    public class Evento : Base
    {
        private readonly List<MembroEvento> _membrosEvento;
        public Evento(string titulo, string descricao, string local, double valor, double? latitude, double? longitude, DateTime data, DateTime? hora, ETipoEvento tipo, long? grupoId, string foto, ECategoriaEvento categoria) : base()
        {
            _membrosEvento = new();
            PreencherEvento(titulo, descricao, local, valor, latitude, longitude, data, hora, tipo, grupoId, foto, categoria);
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
        public ETipoEvento Tipo { get; private set; }
        public long? GrupoId { get; private set; }
        public string Foto { get; private set; }
        public ECategoriaEvento Categoria { get; private set; }
        public IReadOnlyCollection<MembroEvento> MembrosEvento { get => _membrosEvento; }

        public string DiaSemana { get => Data.ToString("dddd"); }
        public bool DiaInteiro { get => Hora == null; }
        public int Confirmados { get => _membrosEvento.Where(m => m.Comparecer == EComparecer.Sim).Count(); }
        public int Pendentes { get => _membrosEvento.Where(m => m.Comparecer == EComparecer.Talvez).Count(); }


        public void PreencherEvento(string titulo, string descricao, string local, double valor, double? latitude, double? longitude, DateTime data, DateTime? hora, ETipoEvento tipo, long? grupoId, string foto, ECategoriaEvento categoria)
        {
            Alterado = DateTime.Now;
            Titulo = titulo;
            Descricao = descricao;
            Cidade = "";
            Local = local;
            Valor = valor;
            Latitude = latitude;
            Longitude = longitude;
            Data = data.Date;
            Hora = hora;
            Tipo = tipo;
            GrupoId = grupoId;
            Foto = foto;
            Categoria = categoria;
            Validate();
        }

        public void Validate() =>
            AddNotifications(new Contract<Notification>[]
            {
                EventoValidation.ValidateTitulo(Titulo)
            });

        public bool MembroEventoExiste(long usuarioId) => _membrosEvento.Any(m => m.UsuarioId == usuarioId);
        public void AdicionarMembroEvento(MembroEvento membroEvento)
        {
            if (!MembroEventoExiste(membroEvento.UsuarioId))
                _membrosEvento.Add(membroEvento);
        }

        public void FazerCheckIn(MembroEvento membroEvento)
        {
            _membrosEvento.FirstOrDefault(a => a.Id == membroEvento.Id || a.UsuarioId == membroEvento.UsuarioId)?.PreencherMembroEvento(membroEvento.UsuarioId, membroEvento.EventoId, membroEvento.Comparecer, membroEvento.Admin);
        }

        public void RemoverMembro(MembroEvento membroEvento)
        {
            if (MembroEventoExiste(membroEvento.UsuarioId))
                _membrosEvento.Remove(membroEvento);
        }
    }
}
