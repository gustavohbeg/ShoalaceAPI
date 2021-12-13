using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoalace.Domain.Commands.Usuario
{
    public class UploadMediaCommand : Command
    {
        public string Base64 { get; set; }
        public string Formato { get; set; }

        public override void Validate() =>
            AddNotifications(new Contract<Command>()
                .IsNotNullOrEmpty(Base64, "Upload.Base64", "Base64 é obrigatório.")
                .IsNotNullOrEmpty(Formato, "Upload.Formato", "Formato é obrigatório.")
            );
    }
}
