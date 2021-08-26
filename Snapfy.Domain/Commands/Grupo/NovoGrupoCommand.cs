using Flunt.Validations;
using Shoalace.Domain.Entities;
using System.Collections.Generic;

namespace Shoalace.Domain.Commands.Grupo
{
    public class NovoGrupoCommand : Command
    {
        public string Nome { get; set; }
        public byte? Foto { get; set; }
        public long UsuarioId { get; set; }
        public List<MembroCommand> Membros { get; set; }

        public override void Validate()
        {
            AddNotifications(new Contract()
                .IsNotNullOrEmpty(Nome, "Grupo.Nome", "Nome é obrigatório.")
                .AreNotEquals(UsuarioId, 0, "Grupo.UsuarioId", "Usuario é obrigatório.")
                );
        }
    }
}
