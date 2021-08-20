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
    public class MensagemRepository : BaseRepository<Mensagem>, IMensagemRepository
    {
        public MensagemRepository(ShoalaceContexto ShoalaceContexto) : base(ShoalaceContexto) { }

        public async Task<List<Mensagem>> ObterPendentesPorUsuario(long usuarioId) =>
            await _ShoalaceContexto.Mensagem.Include(m => m.StatusMensagens).Where(MensagemQuery.ObterPendentesPorUsuario(usuarioId)).AsNoTracking().ToListAsync();

        public async Task<Mensagem> ObterUltimaMensagem(long usuarioId, long contatoId, bool isGrupo) =>
            await _ShoalaceContexto.Mensagem.Where(MensagemQuery.ObterUltimaMensagem(usuarioId, contatoId, isGrupo)).OrderBy(m => m.Cadastro).AsNoTracking().FirstOrDefaultAsync();
    }
}
