using Shoalace.Domain.Commands;
using Shoalace.Domain.Commands.Mensagem;
using Shoalace.Domain.Entities;
using Shoalace.Domain.Enums;
using Shoalace.Domain.Interfaces.Commands;
using Shoalace.Domain.Interfaces.Handlers;
using Shoalace.Domain.Interfaces.Repositories;
using Shoalace.Domain.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shoalace.Domain.Handlers
{
    public class MensagemHandler : IHandler<NovoMensagemCommand>, IHandler<EditarMensagemCommand>
    {
        private readonly IMensagemRepository _mensagemRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IGrupoRepository _grupoRepository;
        public MensagemHandler(IMensagemRepository mensagemRepository, IUsuarioRepository usuarioRepository, IGrupoRepository grupoRepository)
        {
            _mensagemRepository = mensagemRepository;
            _usuarioRepository = usuarioRepository;
            _grupoRepository = grupoRepository;
        }
        public async Task<IResultadoCommand> ManipularAsync(NovoMensagemCommand comando)
        {
            ResultadoCommand retorno = new();

            comando.Validate();
            if (comando.Invalid)
            {
                retorno.AddNotifications(comando);
                return retorno;
            }
            Usuario usuarioOrigem = null;
            Usuario usuarioDestino = null;
            Grupo grupo = null;
            if (comando.UsuarioDestinoId != null && comando.UsuarioDestinoId > 0)
            {
                usuarioOrigem = await _usuarioRepository.ObterPorId(comando.UsuarioId);
                if (usuarioOrigem == null)
                {
                    retorno.AddNotification("Mensagem.UsuarioId", "Usuario não encontrado");
                    return retorno;
                }

                usuarioDestino = await _usuarioRepository.ObterPorId(comando.UsuarioDestinoId.Value);
                if (usuarioDestino == null)
                {
                    retorno.AddNotification("Mensagem.UsuarioDestinoId", "Usuario não encontrado");
                    return retorno;
                }
            }
            else if (comando.GrupoId != null && comando.GrupoId > 0)
            {
                grupo = await _grupoRepository.ObterPorId(comando.GrupoId.Value);

                if (grupo == null)
                {
                    retorno.AddNotification("Mensagem.GrupoId", "Grupo não encontrado");
                    return retorno;
                }
            }

            Mensagem mensagem = new(comando.Texto, comando.UsuarioId, comando.UsuarioDestinoId != null && comando.UsuarioDestinoId > 0 ? comando.UsuarioDestinoId : null, comando.GrupoId != null && comando.GrupoId > 0 ? comando.GrupoId : null, comando.Audio, comando.Foto, EStatus.Enviado);
            retorno.AddNotifications(mensagem);

            if (retorno.Valid)
            {
                await _mensagemRepository.Adicionar(mensagem);
                await _mensagemRepository.Commit();
                retorno.PreencherRetorno(mensagem);
                if (mensagem.UsuarioDestinoId != null && !string.IsNullOrEmpty(usuarioDestino.Token))
                {
                    ExpoService.SendNotification(new List<string>() { usuarioDestino.Token }, usuarioOrigem.Nome, mensagem.Texto);
                }
                else if (mensagem.GrupoId != null)
                {
                    List<string> tokens = new();
                    foreach (Membro membro in grupo.Membros) tokens.Add(membro.Usuario.Token);
                    ExpoService.SendNotification(tokens, usuarioOrigem.Nome, mensagem.Texto);
                }
            }

            return retorno;
        }

        public async Task<IResultadoCommand> ManipularAsync(EditarMensagemCommand comando)
        {
            ResultadoCommand retorno = new();

            comando.Validate();
            if (comando.Invalid)
            {
                retorno.AddNotifications(comando);
                return retorno;
            }

            Usuario usuarioOrigem = null;
            Usuario usuarioDestino = null;
            Grupo grupo = null;
            if (comando.UsuarioDestinoId != null && comando.UsuarioDestinoId > 0)
            {
                usuarioOrigem = await _usuarioRepository.ObterPorId(comando.UsuarioId);
                if (usuarioOrigem == null)
                {
                    retorno.AddNotification("Mensagem.UsuarioId", "Usuario não encontrado");
                    return retorno;
                }

                usuarioDestino = await _usuarioRepository.ObterPorId(comando.UsuarioDestinoId.Value);
                if (usuarioDestino == null)
                {
                    retorno.AddNotification("usuarioDestino.UsuarioId", "Usuario não encontrado");
                    return retorno;
                }
            }
            else if (comando.GrupoId != null && comando.GrupoId > 0)
            {
                grupo = await _grupoRepository.ObterPorId(comando.GrupoId.Value);

                if (grupo == null)
                {
                    retorno.AddNotification("Mensagem.GrupoId", "Grupo não encontrado");
                    return retorno;
                }
            }

            Mensagem mensagem = await _mensagemRepository.ObterPorId(comando.Id);

            if (mensagem == null)
            {
                retorno.AddNotification("Mensagem.Id", "Mensagem não encontrado");
                return retorno;
            }

            mensagem.PreencherMensagem(comando.Texto, comando.UsuarioId, comando.UsuarioDestinoId != null && comando.UsuarioDestinoId > 0 ? comando.UsuarioDestinoId : null, comando.GrupoId != null && comando.GrupoId > 0 ? comando.GrupoId : null, comando.Audio, comando.Foto, comando.Status);
            retorno.AddNotifications(mensagem);

            if (retorno.Valid)
            {
                _mensagemRepository.Atualizar(mensagem);
                await _mensagemRepository.Commit();
                retorno.PreencherRetorno(mensagem);
            }

            return retorno;
        }

        public async Task<IResultadoCommand> ManipularAsync(ExcluirCommand comando)
        {
            ResultadoCommand retorno = new();

            comando.Validate();
            if (comando.Invalid)
            {
                retorno.AddNotifications(comando);
                return retorno;
            }

            Mensagem mensagem = await _mensagemRepository.ObterPorId(comando.Id);

            if (mensagem == null)
            {
                retorno.AddNotification("Mensagem.Id", "Mensagem não encontrado");
                return retorno;
            }

            if (retorno.Valid)
            {
                _mensagemRepository.Remover(mensagem);
                await _mensagemRepository.Commit();
                retorno.PreencherRetorno(mensagem);
            }

            return retorno;
        }

        public async Task<IResultadoCommand> ManipularAsync(NovoListaMensagemCommand comando)
        {
            ResultadoCommand retorno = new();

            comando.Validate();
            if (comando.Invalid)
            {
                retorno.AddNotifications(comando);
                return retorno;
            }

            Mensagem mensagem;
            List<Mensagem> mensagens = new();
            foreach (NovoMensagemCommand mensagemCommand in comando.Mensagens)
            {
                mensagem = new(mensagemCommand.Texto, mensagemCommand.UsuarioId, mensagemCommand.UsuarioDestinoId, mensagemCommand.GrupoId, mensagemCommand.Audio, mensagemCommand.Foto, EStatus.Enviado);
                retorno.AddNotifications(mensagem);
                mensagens.Add(mensagem);
            }

            if (retorno.Valid)
            {
                await _mensagemRepository.AdicionarLista(mensagens);
                await _mensagemRepository.Commit();
            }

            return retorno;
        }
    }
}
