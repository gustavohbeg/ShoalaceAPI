using Shoalace.Domain.Entities;
using System.Threading.Tasks;

namespace Shoalace.Domain.Interfaces.Repositories
{
    public interface IAcessoRepository : IBaseRepository<Acesso>
    {
        Task<Acesso> ObterPorUsuario(long usuarioId);
    }
}
