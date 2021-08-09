using Shoalace.Domain.Enums;
using System;

namespace Shoalace.Domain.Entities
{
    public class StatusMensagem : Base
    {
        public StatusMensagem(long membroId, EStatus status, long mensagemId) : base()
        {
            MembroId = membroId;
            Status = status;
            MensagemId = mensagemId;
        }

        public void PreencherStatusMensagem(long membroId, EStatus status, long mensagemId)
        {
            Alterado = DateTime.Now;
            MembroId = membroId;
            Status = status;
            MensagemId = mensagemId;
        }

        public long MembroId { get; private set; }
        public Membro Membro { get; private set; }
        public EStatus Status { get; private set; }
        public long MensagemId { get; private set; }
        public Mensagem Mensagem { get; private set; }

        public bool Entregue { get => Status == EStatus.Entregue; }
    }
}
