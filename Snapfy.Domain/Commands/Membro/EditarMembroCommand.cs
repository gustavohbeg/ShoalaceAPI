using Flunt.Notifications;
using Flunt.Validations;
using Shoalace.Domain.Validations;

namespace Shoalace.Domain.Commands.Grupo
{
    public class EditarMembroCommand : NovoMembroCommand
    {
        public long Id { get; set; }

        public override void Validate() =>
            AddNotifications(new Contract<Notification>[]
            {
                MembroValidation.ValidateId(Id),
                MembroValidation.ValidateUsuarioId(UsuarioId),
                MembroValidation.ValidateGrupoId(GrupoId)
            });
    }
}
