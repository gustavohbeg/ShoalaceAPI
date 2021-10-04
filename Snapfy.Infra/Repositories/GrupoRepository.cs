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
    public class GrupoRepository : BaseRepository<Grupo>, IGrupoRepository
    {
        public GrupoRepository(ShoalaceContexto ShoalaceContexto) : base(ShoalaceContexto) { }

        public new async Task<Grupo> ObterPorId(long id) =>
            await _ShoalaceContexto.Grupo.Include(g => g.Membros).ThenInclude(m => m.Usuario).Include(g => g.Eventos).ThenInclude(e => e.MembrosEvento).Where(GrupoQuery.ObterPorId(id)).FirstOrDefaultAsync();

        public async Task<ContatoChatResponse> ObterContatoChatPorId(long id) =>
            await _ShoalaceContexto.Grupo.Include(g => g.Membros).ThenInclude(m => m.Usuario).Include(g => g.Eventos).ThenInclude(e => e.MembrosEvento).Where(GrupoQuery.ObterPorId(id))
            .Select(g => new ContatoChatResponse()
            {
                Id = g.Id,
                Nome = g.Nome,
                Foto = g.Foto,
                IsGrupo = true,
                Cadastro = g.Cadastro,
                Mensagens = new List<MensagemResponse>(),
                Membros = g.Membros.Select(m => new MembroResponse()
                {
                    Id = m.Id,
                    UsuarioId = m.UsuarioId,
                    Admin = m.Admin,
                    Usuario = new UsuarioResponse()
                    {
                        Id = m.Usuario.Id,
                        Numero = m.Usuario.Numero,
                        Aniversario = m.Usuario.Aniversario,
                        Sexo = m.Usuario.Sexo,
                        Foto = m.Usuario.Foto,
                        Nome = m.Usuario.Nome,
                        Bio = m.Usuario.Bio,
                        Visto = m.Usuario.Visto,
                        Online = m.Usuario.Online
                    }
                }).OrderBy(m => m.Usuario.Nome).ToList(),
                Eventos = g.Eventos.Select(e => new EventoResponse()
                {
                    Id = e.Id,
                    Titulo = e.Titulo,
                    Descricao = e.Descricao,
                    Cidade = e.Cidade,
                    Local = e.Local,
                    Valor = e.Valor,
                    Latitude = e.Latitude,
                    Longitude = e.Longitude,
                    Data = e.Data,
                    Hora = e.Hora,
                    Tipo = e.Tipo,
                    GrupoId = e.GrupoId,
                    Foto = e.Foto,
                    Categoria = e.Categoria,
                    MembrosEvento = e.MembrosEvento.Select(me => new MembroEventoResponse()
                    {
                        Id = me.Id,
                        UsuarioId = me.UsuarioId,
                        EventoId = me.EventoId,
                        Comparecer = me.Comparecer,
                        Admin = me.Admin,
                        Usuario = new UsuarioResponse()
                        {
                            Id = me.Usuario.Id,
                            Numero = me.Usuario.Numero,
                            Aniversario = me.Usuario.Aniversario,
                            Sexo = me.Usuario.Sexo,
                            Foto = me.Usuario.Foto,
                            Nome = me.Usuario.Nome,
                            Bio = me.Usuario.Bio,
                            Visto = me.Usuario.Visto,
                            Online = me.Usuario.Online
                        }
                    }).ToList()
                }).OrderBy(m => m.Data).ToList()
            }).FirstOrDefaultAsync();

        public async Task<List<Grupo>> ObterTodos(long usuarioId) =>
            await _ShoalaceContexto.Grupo.Include(g => g.Membros).ThenInclude(m => m.Usuario).Include(g => g.Eventos).ThenInclude(e => e.MembrosEvento).Where(GrupoQuery.ObterTodosPorUsuario(usuarioId)).AsNoTracking().ToListAsync();

        public async Task<List<Grupo>> ObterTodosPorUsuario(long usuarioId) =>
            await _ShoalaceContexto.Grupo.Include(g => g.Membros).ThenInclude(m => m.Usuario).Include(g => g.Eventos).ThenInclude(e => e.MembrosEvento).Where(GrupoQuery.ObterTodosPorUsuario(usuarioId)).AsNoTracking().ToListAsync();
    }
}
