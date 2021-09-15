using Microsoft.EntityFrameworkCore;
using Shoalace.Domain.Entities;
using Shoalace.Domain.Enums;
using Shoalace.Domain.Interfaces.Repositories;
using Shoalace.Domain.Queries;
using Shoalace.Infra.Contexto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shoalace.Infra.Repositories
{
    public class EventoRepository : BaseRepository<Evento>, IEventoRepository
    {
        public EventoRepository(ShoalaceContexto ShoalaceContexto) : base(ShoalaceContexto) { }

        public new async Task<Evento> ObterPorId(long id) =>
            await _ShoalaceContexto.Evento.Include(e => e.MembrosEvento).ThenInclude(m => m.Usuario).Where(EventoQuery.ObterPorId(id)).FirstOrDefaultAsync();

        public async Task<List<Evento>> ObterTodosPorUsuario(long usuarioId) =>
            await _ShoalaceContexto.Evento.Include(e => e.MembrosEvento).ThenInclude(m => m.Usuario).Where(EventoQuery.ObterTodosPorUsuario(usuarioId)).AsNoTracking().ToListAsync();

        public async Task<List<Evento>> ObterPor2Usuarios(long usuarioId, long contatoId) =>
            await _ShoalaceContexto.Evento.Include(e => e.MembrosEvento).ThenInclude(m => m.Usuario).Where(EventoQuery.ObterPor2Usuarios(usuarioId, contatoId)).AsNoTracking().ToListAsync();

        public async Task<List<Evento>> ObterProximosPorUsuario(long usuarioId) =>
            await _ShoalaceContexto.Evento.Include(e => e.MembrosEvento).ThenInclude(m => m.Usuario).Where(EventoQuery.ObterProximosPorUsuario(usuarioId)).AsNoTracking().ToListAsync();

        public async Task<List<Evento>> ObterTodosExplorar() =>
            await _ShoalaceContexto.Evento.Include(e => e.MembrosEvento).ThenInclude(m => m.Usuario).Where(EventoQuery.ObterTodosExplorar()).AsNoTracking().ToListAsync();

        public async Task<List<Evento>> ObterTodosPorData(DateTime data) =>
            await _ShoalaceContexto.Evento.Include(e => e.MembrosEvento).ThenInclude(m => m.Usuario).Where(EventoQuery.ObterTodosPorData(data)).AsNoTracking().ToListAsync();

        public async Task<List<Evento>> ObterPorCategoriaECidade(ECategoria categoria, string cidade) =>
            await _ShoalaceContexto.Evento.Include(e => e.MembrosEvento).ThenInclude(m => m.Usuario).Where(EventoQuery.ObterPorCategoriaECidade(categoria, cidade)).AsNoTracking().ToListAsync();
    }
}
