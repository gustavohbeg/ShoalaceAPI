using Shoalace.Domain.Entities;
using Shoalace.Domain.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shoalace.Domain.Interfaces.Repositories
{
    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {
        Task<Usuario> ObterPorNumero(long numero);
        Task<ContatoChatResponse> ObterContatoChatPorId(long id);
        Task<List<Usuario>> ObterContatos(long id);
    }
}
