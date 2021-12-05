using Shoalace.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Shoalace.Domain.Queries
{
    public static class UsuarioQuery
    {
        public static Expression<Func<Usuario, bool>> ObterPorNumero(string numero) =>
            u => u.Numero == numero;

        public static Expression<Func<Usuario, bool>> ObterPorId(long id) =>
            u => u.Id == id;

        public static Expression<Func<Usuario, bool>> ObterContatos(long id) =>
            u => u.Id != id;
    }
}
