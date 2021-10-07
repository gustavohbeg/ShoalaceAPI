using Shoalace.Domain.Entities;
using Shoalace.Domain.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shoalace.Domain.Interfaces.Repositories
{
    public interface IGrupoRepository : IBaseRepository<Grupo>
    {
        Task<ContatoChatResponse> ObterContatoChatPorId(long id);
        Task<List<Grupo>> ObterTodos(long usuarioId);
        Task<List<Grupo>> ObterTodosPorUsuario(long usuarioId);
    }
}
