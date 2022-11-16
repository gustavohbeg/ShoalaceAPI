using Microsoft.EntityFrameworkCore;
using Shoalace.Domain.Entities;
using Shoalace.Domain.Enums;
using Shoalace.Domain.Interfaces.Repositories;
using Shoalace.Domain.Queries;
using Shoalace.Domain.Responses;
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
            await _ShoalaceContexto.Evento.Include(e => e.MembrosEvento).ThenInclude(m => m.Usuario).FirstOrDefaultAsync(EventoQuery.ObterPorId(id));

        public async Task<List<Evento>> ObterTodosPorUsuario(long usuarioId) =>
            await _ShoalaceContexto.Evento.Include(e => e.MembrosEvento).ThenInclude(m => m.Usuario).Where(EventoQuery.ObterTodosPorUsuario(usuarioId)).AsNoTracking().OrderBy(m => m.Data).ToListAsync();

        public async Task<List<Evento>> ObterPor2Usuarios(long usuarioId, long contatoId) =>
            await _ShoalaceContexto.Evento.Include(e => e.MembrosEvento).ThenInclude(m => m.Usuario).Where(EventoQuery.ObterPor2Usuarios(usuarioId, contatoId)).AsNoTracking().OrderBy(m => m.Data).ToListAsync();

        public async Task<List<EventoResponse>> ObterResponsesPor2Usuarios(long usuarioId, long contatoId) =>
            (await ObterPor2Usuarios(usuarioId, contatoId)).Select(e => new EventoResponse()
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
                MembrosEvento = e.MembrosEvento.Select(m => new MembroEventoResponse()
                {
                    Id = m.Id,
                    EventoId = m.EventoId,
                    UsuarioId = m.UsuarioId,
                    Comparecer = m.Comparecer,
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
                }).ToList()

            }).ToList();

        public async Task<List<Evento>> ObterProximosPorUsuario(long usuarioId) =>
            await _ShoalaceContexto.Evento.Include(e => e.MembrosEvento).ThenInclude(m => m.Usuario).Where(EventoQuery.ObterProximosPorUsuario(usuarioId)).AsNoTracking().OrderBy(m => m.Data).ToListAsync();

        public async Task<List<Evento>> ObterTodosExplorar() =>
            await _ShoalaceContexto.Evento.Include(e => e.MembrosEvento).ThenInclude(m => m.Usuario).Where(EventoQuery.ObterTodosExplorar()).AsNoTracking().OrderBy(m => m.Data).ToListAsync();

        public async Task<List<Evento>> ObterTodosPorData(DateTime data) =>
            await _ShoalaceContexto.Evento.Include(e => e.MembrosEvento).ThenInclude(m => m.Usuario).Where(EventoQuery.ObterTodosPorData(data)).AsNoTracking().ToListAsync();

        public async Task<List<Evento>> ObterPorCategoriaECidade(ECategoriaEvento categoria, string cidade) =>
            await _ShoalaceContexto.Evento.Include(e => e.MembrosEvento).ThenInclude(m => m.Usuario).Where(EventoQuery.ObterPorCategoriaECidade(categoria, cidade)).AsNoTracking().OrderBy(m => m.Data).ToListAsync();
    }
}
