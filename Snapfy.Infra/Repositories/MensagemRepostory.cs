﻿using Microsoft.EntityFrameworkCore;
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
    public class MensagemRepository : BaseRepository<Mensagem>, IMensagemRepository
    {
        public MensagemRepository(ShoalaceContexto ShoalaceContexto) : base(ShoalaceContexto) { }

        public async Task<List<Mensagem>> ObterPendentesPorUsuario(long usuarioId) =>
            await _ShoalaceContexto.Mensagem.Include(m => m.StatusMensagens).Where(MensagemQuery.ObterPendentesPorUsuario(usuarioId)).AsNoTracking().ToListAsync();

        public async Task<List<Mensagem>> ObterTodosPorUsuario(long usuarioId, long contatoId) =>
            await _ShoalaceContexto.Mensagem.Include(m => m.StatusMensagens).Where(MensagemQuery.ObterPorUsuario(usuarioId, contatoId)).AsNoTracking().ToListAsync();

        public async Task<List<MensagemResponse>> ObterTodosResponsePorUsuario(long usuarioId, long contatoId) =>
            (await ObterTodosPorUsuario(usuarioId, contatoId)).Select(m => new MensagemResponse
            (
                m.Id,
                m.Texto,
                m.UsuarioId,
                m.UsuarioDestinoId,
                m.GrupoId,
                m.Audio,
                m.Foto,
                m.Status,
                m.Cadastro
            )).ToList();

        public async Task<List<Mensagem>> ObterTodosPorGrupo(long grupoId) =>
            await _ShoalaceContexto.Mensagem.Include(m => m.StatusMensagens).Where(MensagemQuery.ObterPorGrupo(grupoId)).AsNoTracking().ToListAsync();

        public async Task<Mensagem> ObterUltimaMensagem(long usuarioId, long contatoId, bool isGrupo) =>
            await _ShoalaceContexto.Mensagem.Include(m => m.StatusMensagens).Where(MensagemQuery.ObterPorContato(usuarioId, contatoId, isGrupo)).OrderByDescending(m => m.Cadastro).AsNoTracking().FirstOrDefaultAsync();

        public async Task<MensagemResponse> ObterUltimaMensagemResponse(long usuarioId, long contatoId, bool isGrupo)
        {
            Mensagem mensagem = await ObterUltimaMensagem(usuarioId, contatoId, isGrupo);
            return mensagem != null ? new MensagemResponse
            (
                mensagem.Id,
                mensagem.Texto,
                mensagem.UsuarioId,
                mensagem.UsuarioDestinoId,
                mensagem.GrupoId,
                mensagem.Audio,
                mensagem.Foto,
                mensagem.Status,
                mensagem.Cadastro
            )
            :null;
        }

        public async Task<List<Mensagem>> ObterNaoLidasPorContato(long usuarioId, long contatoId) =>
            await _ShoalaceContexto.Mensagem.Where(MensagemQuery.ObterNaoLidasPorContato(usuarioId, contatoId)).ToListAsync();

        public async Task<List<Mensagem>> ObterNaoLidasPorGrupo(long usuarioId, long contatoId) =>
            await _ShoalaceContexto.Mensagem.Where(MensagemQuery.ObterNaoLidasPorGrupo(usuarioId, contatoId)).ToListAsync();
    }
}
