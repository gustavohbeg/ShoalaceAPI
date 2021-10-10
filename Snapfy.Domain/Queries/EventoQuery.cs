using Shoalace.Domain.Entities;
using Shoalace.Domain.Enums;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Shoalace.Domain.Queries
{
    public static class EventoQuery
    {
        public static Expression<Func<Evento, bool>> ObterPorId(long id) =>
            e => e.Id == id;

        public static Expression<Func<Evento, bool>> ObterTodosPorUsuario(long usuarioId) =>
            e => e.MembrosEvento != null && e.MembrosEvento.Any(m => m.UsuarioId == usuarioId);

        public static Expression<Func<Evento, bool>> ObterPor2Usuarios(long usuarioId, long contatoId) =>
            e => e.MembrosEvento != null && e.GrupoId == null && e.MembrosEvento.Count() == 2 && e.MembrosEvento.Any(m => m.UsuarioId == contatoId) && e.MembrosEvento.Any(m => m.UsuarioId == usuarioId);

        public static Expression<Func<Evento, bool>> ObterProximosPorUsuario(long usuarioId) =>
            e => e.Data >= DateTime.Now.AddHours(-5) && e.MembrosEvento != null && e.MembrosEvento.Any(m => m.UsuarioId == usuarioId);

        public static Expression<Func<Evento, bool>> ObterTodosExplorar() =>
           e => e.Categoria != ECategoria.Privado && e.Data >= DateTime.Now;

        public static Expression<Func<Evento, bool>> ObterTodosPorData(DateTime data) =>
            e => e.Data == data;

        public static Expression<Func<Evento, bool>> ObterPorCategoriaECidade(ECategoria categoria, string cidade) =>
            e => (e.Categoria == categoria || categoria == ECategoria.Publico) && (e.Cidade == cidade || cidade == "");
    }
}
