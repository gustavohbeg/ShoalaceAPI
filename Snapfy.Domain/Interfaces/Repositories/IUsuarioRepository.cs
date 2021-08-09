using Shoalace.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shoalace.Domain.Interfaces.Repositories
{
    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {
        Task<List<Usuario>> ObterContatos(long id);
    }
}
