using Shoalace.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shoalace.Domain.Interfaces.Repositories
{
    public interface IGrupoRepository : IBaseRepository<Grupo>
    {
        new Task<Grupo> ObterPorId(long id);
        Task<List<Grupo>> ObterTodosPorUsuario(long usuarioId);
    }
}
