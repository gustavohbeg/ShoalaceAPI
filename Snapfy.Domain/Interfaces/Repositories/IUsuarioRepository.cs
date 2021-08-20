using Shoalace.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shoalace.Domain.Interfaces.Repositories
{
    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {
        Task<Usuario> ObterPorNumero(long numero);
        Task<List<Usuario>> ObterContatos(long id);
    }
}
