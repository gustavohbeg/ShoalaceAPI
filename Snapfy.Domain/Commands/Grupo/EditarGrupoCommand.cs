﻿using Flunt.Notifications;
using Flunt.Validations;
using Shoalace.Domain.Entities;
using Shoalace.Domain.Validations;
using System.Collections.Generic;

namespace Shoalace.Domain.Commands.Grupo
{
    public class EditarGrupoCommand : NovoGrupoCommand
    {
        public long Id { get; set; }

        public override void Validate() =>
            AddNotifications(new Contract<Notification>[]
            {
                GrupoValidation.ValidateId(Id),
                GrupoValidation.ValidateNome(Nome)
            });
    }
}
