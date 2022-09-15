using Flunt.Notifications;
using Flunt.Validations;
using Shoalace.Domain.Validations;
using System;

namespace Shoalace.Domain.Entities
{
    public class Membro : Base
    {
        public Membro(long usuarioId, long grupoId, bool admin) : base()
        {
            PreencherMembro(usuarioId, grupoId, admin);
        }

        public void PreencherMembro(long usuarioId, long grupoId, bool admin)
        {
            Alterado = DateTime.Now;
            UsuarioId = usuarioId;
            GrupoId = grupoId;
            Admin = admin;
            Validate();
        }

        public void Validate() =>
            AddNotifications(new Contract<Notification>[]
            {
                MembroValidation.ValidateUsuarioId(UsuarioId),
                MembroValidation.ValidateGrupoId(GrupoId)
            });

        public long UsuarioId { get; private set; }
        public Usuario Usuario { get; private set; }
        public long GrupoId { get; private set; }
        public bool Admin { get; private set; }

        public void SetarAdmin(bool admin) => Admin = admin;
    }
}
