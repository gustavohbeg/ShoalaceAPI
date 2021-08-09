using Shoalace.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Shoalace.Domain.Queries
{
    public static class AcessoQuery
    {
        public static Expression<Func<Acesso, bool>> ObterPorUsuario(long usuarioId) =>
            u => u.UsuarioId == usuarioId;
    }
}
