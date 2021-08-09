using Microsoft.EntityFrameworkCore;
using Shoalace.Domain.Entities;
using Shoalace.Domain.Interfaces.Repositories;
using Shoalace.Domain.Queries;
using Shoalace.Infra.Contexto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shoalace.Infra.Repositories
{
    public class GrupoRepository : BaseRepository<Grupo>, IGrupoRepository
    {
        public GrupoRepository(ShoalaceContexto ShoalaceContexto) : base(ShoalaceContexto) { }

        public new async Task<Grupo> ObterPorId(long id) =>
            await _ShoalaceContexto.Grupo.Include(g => g.Membros).Where(GrupoQuery.ObterPorId(id)).FirstOrDefaultAsync();

        public async Task<List<Grupo>> ObterTodosPorUsuario(long usuarioId) =>
            await _ShoalaceContexto.Grupo.Include(g => g.Membros).Where(GrupoQuery.ObterTodosPorUsuario(usuarioId)).ToListAsync();
    }
}
