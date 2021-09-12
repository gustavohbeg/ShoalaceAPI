using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoalace.Domain.Commands.Usuario
{
    public class UploadImageCommand : Command
    {
        public string Base64 { get; set; }

        public override void Validate()
        {
            AddNotifications(new Contract()
                .IsNotNullOrEmpty(Base64, "Base64.Base64", "Base64 é obrigatório.")
                );
        }
    }
}
