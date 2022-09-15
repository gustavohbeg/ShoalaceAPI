using Microsoft.EntityFrameworkCore;
using Shoalace.Domain.Entities;
using Shoalace.Domain.Interfaces.Repositories;
using Shoalace.Domain.Queries;
using Shoalace.Domain.Responses;
using Shoalace.Infra.Contexto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shoalace.Infra.Repositories
{
    public class ContatoRepository : BaseRepository<Contato>, IContatoRepository
    {
        public ContatoRepository(ShoalaceContexto ShoalaceContexto) : base(ShoalaceContexto) { }

        public async Task<List<Contato>> ObterContatosPorUsuario(long usuarioId) =>
            await _ShoalaceContexto.Contato.Where(ContatoQuery.ObterPorUsuarioId(usuarioId)).AsNoTracking().ToListAsync();

        public async Task<Contato> ObterPorNumero(long usuarioId, string numero) =>
            await _ShoalaceContexto.Contato.FirstOrDefaultAsync(ContatoQuery.ObterPorNumero(usuarioId, numero));
    }
}
