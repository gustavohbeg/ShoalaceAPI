﻿using Flunt.Notifications;
using Flunt.Validations;
using Shoalace.Domain.Enums;
using Shoalace.Domain.Validations;
using System;
using System.Collections.Generic;

namespace Shoalace.Domain.Commands.Evento
{
    public class EditarEventoCommand : NovoEventoCommand
    {
        public long Id { get; set; }

        public override void Validate() => 
            AddNotifications(new Contract<Notification>[]
            {
                EventoValidation.ValidateId(Id),
                EventoValidation.ValidateTitulo(Titulo)
            });
    }
}
