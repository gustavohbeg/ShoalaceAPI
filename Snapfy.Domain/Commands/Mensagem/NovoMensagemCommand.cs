using Shoalace.Domain.Enums;

namespace Shoalace.Domain.Commands.Mensagem
{
    public class NovoMensagemCommand : Command
    {
        public string Texto { get; private set; }
        public long UsuarioId { get; private set; }
        public long UsuarioDestinoId { get; private set; }
        public long GrupoId { get; private set; }
        public byte? Audio { get; private set; }
        public byte? Foto { get; private set; }
        public EStatus Status { get; private set; }

        public override void Validate()
        {

        }
    }
}
