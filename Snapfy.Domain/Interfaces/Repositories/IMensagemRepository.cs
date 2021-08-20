using Shoalace.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shoalace.Domain.Interfaces.Repositories
{
    public interface IMensagemRepository : IBaseRepository<Mensagem>
    {
        Task<List<Mensagem>> ObterPendentesPorUsuario(long usuarioId);
        Task<Mensagem> ObterUltimaMensagem(long usuarioId, long contatoId, bool isGrupo);
    }
}
