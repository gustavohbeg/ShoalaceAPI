using Flunt.Validations;
using Shoalace.Domain.Enums;

namespace Shoalace.Domain.Commands.Mensagem
{
    public class EditarMensagemCommand : Command
    {
        public long Id { get; set; }
        public string Texto { get; set; }
        public long UsuarioId { get; set; }
        public long? UsuarioDestinoId { get; set; }
        public long? GrupoId { get; set; }
        public string Audio { get; set; }
        public string Foto { get; set; }
        public EStatus Status { get; set; }

        public override void Validate()
        {
            AddNotifications(new Contract()
                .AreNotEquals(Id, 0, "Mensagem.Id", "Mensagem é obrigatório.")
                );
        }
    }
}
