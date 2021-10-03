using Shoalace.Domain.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Shoalace.Domain.Queries
{
    public static class GrupoQuery
    {
        public static Expression<Func<Grupo, bool>> ObterPorId(long id) =>
            g => g.Id == id;

        public static Expression<Func<Grupo, bool>> ObterTodosPorUsuario(long usuarioId) =>
            g => g.Membros != null && g.Membros.Any(m => m.UsuarioId == usuarioId);
    }
}
