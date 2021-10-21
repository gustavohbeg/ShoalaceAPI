using Microsoft.EntityFrameworkCore;
using Shoalace.Domain.Entities;
using Shoalace.Domain.Enums;
using Shoalace.Domain.Interfaces.Repositories;
using Shoalace.Domain.Queries;
using Shoalace.Domain.Responses;
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
            await _ShoalaceContexto.Grupo.Include(g => g.Membros).ThenInclude(m => m.Usuario).Include(g => g.Eventos).ThenInclude(e => e.MembrosEvento).Where(GrupoQuery.ObterPorId(id)).FirstOrDefaultAsync();

        public async Task<ContatoChatResponse> ObterContatoChatPorId(long id) =>
            await _ShoalaceContexto.Grupo.Include(g => g.Membros).ThenInclude(m => m.Usuario).Include(g => g.Eventos).ThenInclude(e => e.MembrosEvento).Where(GrupoQuery.ObterPorId(id))
            .Select(g => new ContatoChatResponse
            (
                g.Id,
                0,
                g.Nome,
                g.Foto,
                "",
                g.Cadastro,
                ESexo.Masculino,
                true,
                g.Cadastro,
                new List<MensagemResponse>(),
                g.Membros.Select(m => new MembroResponse
                (
                    m.Id,
                    m.UsuarioId,
                    m.Admin,
                    new UsuarioResponse
                    (
                        m.Usuario.Id,
                        m.Usuario.Numero,
                        m.Usuario.Aniversario,
                        m.Usuario.Sexo,
                        m.Usuario.Foto,
                        m.Usuario.Nome,
                        m.Usuario.Bio,
                        m.Usuario.Visto,
                        m.Usuario.Online
                    )
                )).OrderBy(m => m.Usuario.Nome).ToList(),
                g.Eventos.Select(e => new EventoResponse
                (
                    e.Id,
                    e.Titulo,
                    e.Descricao,
                    e.Cidade,
                    e.Local,
                    e.Valor,
                    e.Latitude,
                    e.Longitude,
                    e.Data,
                    e.Hora,
                    e.Tipo,
                    e.GrupoId,
                    e.Foto,
                    e.Categoria,
                    e.MembrosEvento.Select(me => new MembroEventoResponse
                    (
                        me.Id,
                        me.UsuarioId,
                        me.EventoId,
                        me.Comparecer,
                        me.Admin,
                        new UsuarioResponse
                        (
                            me.Usuario.Id,
                            me.Usuario.Numero,
                            me.Usuario.Aniversario,
                            me.Usuario.Sexo,
                            me.Usuario.Foto,
                            me.Usuario.Nome,
                            me.Usuario.Bio,
                            me.Usuario.Visto,
                            me.Usuario.Online
                        )
                    )).ToList()
                )).OrderBy(m => m.Data).ToList()
            )).FirstOrDefaultAsync();

        public async Task<List<Grupo>> ObterTodos(long usuarioId) =>
            await _ShoalaceContexto.Grupo.Include(g => g.Membros).ThenInclude(m => m.Usuario).Include(g => g.Eventos).ThenInclude(e => e.MembrosEvento).Where(GrupoQuery.ObterTodosPorUsuario(usuarioId)).AsNoTracking().ToListAsync();

        public async Task<List<Grupo>> ObterTodosPorUsuario(long usuarioId) =>
            await _ShoalaceContexto.Grupo.Include(g => g.Membros).ThenInclude(m => m.Usuario).Include(g => g.Eventos).ThenInclude(e => e.MembrosEvento).Where(GrupoQuery.ObterTodosPorUsuario(usuarioId)).AsNoTracking().ToListAsync();
    }
}
