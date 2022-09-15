using Shoalace.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Shoalace.Domain.Queries
{
    public static class ContatoQuery
    {
        public static Expression<Func<Contato, bool>> ObterPorNumero(long usuarioId, string numero) =>
            u => u.UsuarioId == usuarioId && u.Numero == numero;

        public static Expression<Func<Contato, bool>> ObterPorUsuarioId(long id) =>
            u => u.UsuarioId == id;
    }
}
