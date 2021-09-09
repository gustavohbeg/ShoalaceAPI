using Flunt.Validations;
using Shoalace.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Shoalace.Domain.Commands.Evento
{
    public class NovoEventoCommand : Command
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
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
        public List<MembroEventoCommand> Membros { get; set; }

        public override void Validate()
        {
            AddNotifications(new Contract()
                .IsNotNullOrEmpty(Titulo, "Evento.Titulo", "Titulo é obrigatório.")
                .IsNotNull(Tipo, "Evento.Tipo", "Tipo é obrigatório")
                .IsNotNull(Data, "Evento.Data", "Data é obrigatório")
                );
        }
    }
}
