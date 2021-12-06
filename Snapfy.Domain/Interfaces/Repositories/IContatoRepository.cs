using Shoalace.Domain.Entities;
using Shoalace.Domain.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shoalace.Domain.Interfaces.Repositories
{
    public interface IContatoRepository : IBaseRepository<Contato>
    {
        Task<List<Contato>> ObterContatosPorUsuario(long usuarioId);
        Task<Contato> ObterPorNumero(long usuarioId, string numero);
    }
}
