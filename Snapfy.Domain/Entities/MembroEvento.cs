﻿using Flunt.Notifications;
using Flunt.Validations;
using Shoalace.Domain.Enums;
using Shoalace.Domain.Validations;
using System;

namespace Shoalace.Domain.Entities
{
    public class MembroEvento : Base
    {
        public MembroEvento(long usuarioId, long eventoId, EComparecer comparecer, bool admin) : base()
        {
            PreencherMembroEvento(usuarioId, eventoId, comparecer, admin);
        }

        public void PreencherMembroEvento(long usuarioId, long eventoId, EComparecer comparecer, bool admin)
        {
            Alterado = DateTime.Now;
            UsuarioId = usuarioId;
            EventoId = eventoId;
            Comparecer = comparecer;
            Admin = admin;
            Validate();
        }

        public void Validate() =>
            AddNotifications(new Contract<Notification>[]
            {
                MembroEventoValidation.ValidateUsuarioId(UsuarioId),
                MembroEventoValidation.ValidateEventoId(EventoId)
            });

        public long UsuarioId { get; private set; }
        public Usuario Usuario { get; private set; }
        public long EventoId { get; private set; }
        public EComparecer Comparecer { get; private set; }
        public bool Admin { get; private set; }

        public void SetarAdmin(bool admin) => Admin = admin;
    }
}
