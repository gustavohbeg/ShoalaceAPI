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
        }

        public void Validar()
        {
            if (UsuarioId == 0)
                AddNotification("Membro.UsuarioId", "Usuario é obrigatório");

            if (GrupoId == 0)
                AddNotification("Membro.GrupoId", "Grupo é obrigatório");
        }

        public long UsuarioId { get; private set; }
        public Usuario Usuario { get; private set; }
        public long GrupoId { get; private set; }
        public bool Admin { get; private set; }

        public void SetarAdmin(bool admin) => Admin = admin;
    }
}
