using Shoalace.Domain.Enums;
using System;

namespace Shoalace.Domain.Entities
{
    public class StatusMensagem : Base
    {
        public StatusMensagem(long membroId, EStatusMensagem status, long mensagemId) : base()
        {
            PreencherStatusMensagem(membroId, status, mensagemId);
        }

        public void PreencherStatusMensagem(long membroId, EStatusMensagem status, long mensagemId)
        {
            Alterado = DateTime.Now;
            MembroId = membroId;
            Status = status;
            MensagemId = mensagemId;
        }

        public long MembroId { get; private set; }
        public Membro Membro { get; private set; }
        public EStatusMensagem Status { get; private set; }
        public long MensagemId { get; private set; }
        public Mensagem Mensagem { get; private set; }
    }
}
