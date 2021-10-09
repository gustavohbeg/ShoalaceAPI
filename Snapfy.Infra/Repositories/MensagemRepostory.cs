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
    public class MensagemRepository : BaseRepository<Mensagem>, IMensagemRepository
    {
        public MensagemRepository(ShoalaceContexto ShoalaceContexto) : base(ShoalaceContexto) { }

        public async Task<List<Mensagem>> ObterPendentesPorUsuario(long usuarioId) =>
            await _ShoalaceContexto.Mensagem.Include(m => m.StatusMensagens).Where(MensagemQuery.ObterPendentesPorUsuario(usuarioId)).AsNoTracking().ToListAsync();

        public async Task<List<Mensagem>> ObterTodosPorUsuario(long usuarioId, long contatoId) =>
            await _ShoalaceContexto.Mensagem.Include(m => m.StatusMensagens).Where(MensagemQuery.ObterPorUsuario(usuarioId, contatoId)).AsNoTracking().ToListAsync();

        public async Task<List<MensagemResponse>> ObterTodosResponsePorUsuario(long usuarioId, long contatoId) =>
            (await ObterTodosPorUsuario(usuarioId, contatoId)).Select(m => new MensagemResponse()
            {
                Id = m.Id,
                Texto = m.Texto,
                UsuarioId = m.UsuarioId,
                UsuarioDestinoId = m.UsuarioDestinoId,
                GrupoId = m.GrupoId,
                Audio = m.Audio,
                Foto = m.Foto,
                Status = m.Status,
                Cadastro = m.Cadastro
            }).ToList();

        public async Task<List<Mensagem>> ObterTodosPorGrupo(long grupoId) =>
            await _ShoalaceContexto.Mensagem.Include(m => m.StatusMensagens).Where(MensagemQuery.ObterPorGrupo(grupoId)).AsNoTracking().ToListAsync();

        public async Task<Mensagem> ObterUltimaMensagem(long usuarioId, long contatoId, bool isGrupo) =>
            await _ShoalaceContexto.Mensagem.Include(m => m.StatusMensagens).Where(MensagemQuery.ObterPorContato(usuarioId, contatoId, isGrupo)).OrderByDescending(m => m.Cadastro).AsNoTracking().FirstOrDefaultAsync();

        public async Task<MensagemResponse> ObterUltimaMensagemResponse(long usuarioId, long contatoId, bool isGrupo)
        {
            Mensagem mensagem = await ObterUltimaMensagem(usuarioId, contatoId, isGrupo);
            return new MensagemResponse()
            {
                Id = mensagem.Id,
                Texto = mensagem.Texto,
                UsuarioId = mensagem.UsuarioId,
                UsuarioDestinoId = mensagem.UsuarioDestinoId,
                GrupoId = mensagem.GrupoId,
                Audio = mensagem.Audio,
                Foto = mensagem.Foto,
                Status = mensagem.Status,
                Cadastro = mensagem.Cadastro
            };
        }
    }
}
