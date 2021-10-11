using Shoalace.Domain.Entities;
using Shoalace.Domain.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shoalace.Domain.Interfaces.Repositories
{
    public interface IMensagemRepository : IBaseRepository<Mensagem>
    {
        Task<List<Mensagem>> ObterPendentesPorUsuario(long usuarioId);
        Task<Mensagem> ObterUltimaMensagem(long usuarioId, long contatoId, bool isGrupo);
        Task<MensagemResponse> ObterUltimaMensagemResponse(long usuarioId, long contatoId, bool isGrupo);
        Task<List<Mensagem>> ObterTodosPorUsuario(long usuarioId, long contatoId);
        Task<List<MensagemResponse>> ObterTodosResponsePorUsuario(long usuarioId, long contatoId);
        Task<List<Mensagem>> ObterTodosPorGrupo(long grupoId);
        Task<List<Mensagem>> ObterNaoLidasPorContato(long usuarioId, long contatoId);
        Task<List<Mensagem>> ObterNaoLidasPorGrupo(long usuarioId, long grupoId);
    }
}
