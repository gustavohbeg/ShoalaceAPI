using Shoalace.Domain.Entities;
using Shoalace.Domain.Enums;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Shoalace.Domain.Queries
{
    public static class MensagemQuery
    {
        public static Expression<Func<Mensagem, bool>> ObterPorId(long id) =>
            m => m.Id == id;

        public static Expression<Func<Mensagem, bool>> ObterPorUsuario(long usuarioId, long contatoId) =>
            m => (m.UsuarioId == usuarioId && m.UsuarioDestinoId == contatoId) || (m.UsuarioId == contatoId && m.UsuarioDestinoId == usuarioId);

        public static Expression<Func<Mensagem, bool>> ObterPorGrupo(long grupoId) =>
            m => m.GrupoId == grupoId;

        public static Expression<Func<Mensagem, bool>> ObterPorContato(long usuarioId, long contatoId, bool isGrupo) =>
            m => (isGrupo && m.GrupoId == contatoId) || (!isGrupo && (m.UsuarioId == usuarioId && m.UsuarioDestinoId == contatoId) || (m.UsuarioId == contatoId && m.UsuarioDestinoId == usuarioId));

        public static Expression<Func<Mensagem, bool>> ObterPendentesPorUsuario(long usuarioId) =>
            m => (m.UsuarioDestinoId == usuarioId && m.Status == EStatus.Enviado) || (m.StatusMensagens != null && m.StatusMensagens.Any(s => s.Membro.UsuarioId == usuarioId));
    }
}
