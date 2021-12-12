using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoalace.Domain.Commands.Mensagem
{
    public class UploadVideoCommand : Command
    {
        public string Base64 { get; set; }

        public override void Validate() =>
            AddNotifications(new Contract<Command>()
                .IsNotNullOrEmpty(Base64, "Base64.Base64", "Base64 é obrigatório.")
            );
    }
}
