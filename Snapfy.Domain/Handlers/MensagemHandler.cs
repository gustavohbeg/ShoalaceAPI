using Shoalace.Domain.Commands;
using Shoalace.Domain.Commands.Mensagem;
using Shoalace.Domain.Entities;
using Shoalace.Domain.Enums;
using Shoalace.Domain.Interfaces.Commands;
using Shoalace.Domain.Interfaces.Handlers;
using Shoalace.Domain.Interfaces.Repositories;
using Shoalace.Domain.Interfaces.Services;
using Shoalace.Domain.Responses;
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
        private readonly IFileUpload _fileUpload;
        public MensagemHandler(IMensagemRepository mensagemRepository, IUsuarioRepository usuarioRepository, IGrupoRepository grupoRepository, IFileUpload fileUpload)
        {
            _mensagemRepository = mensagemRepository;
            _usuarioRepository = usuarioRepository;
            _grupoRepository = grupoRepository;
            _fileUpload = fileUpload;
        }

        //NOVO MENSAGEM
        public async Task<IResultadoCommand> ManipularAsync(NovoMensagemCommand comando)
        {
            ResultadoCommand retorno = new();

            comando.Validate();
            if (comando.Invalid)
            {
                retorno.AddNotifications(comando);
                return retorno;
            }

            Usuario usuarioOrigem = await _usuarioRepository.ObterPorId(comando.UsuarioId);
            if (usuarioOrigem == null)
            {
                retorno.AddNotification("Mensagem.UsuarioId", "Usuario não encontrado");
                return retorno;
            }

            Usuario usuarioDestino = null;
            Grupo grupo = null;
            if (comando.UsuarioDestinoId != null && comando.UsuarioDestinoId > 0)
            {
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

            Mensagem mensagem = new(comando.Texto, comando.UsuarioId, comando.UsuarioDestinoId != null && comando.UsuarioDestinoId > 0 ? comando.UsuarioDestinoId : null, comando.GrupoId != null && comando.GrupoId > 0 ? comando.GrupoId : null, comando.Audio, comando.Foto, EStatusMensagem.Enviado);
            mensagem.Validate();
            retorno.AddNotifications(mensagem);

            if (retorno.Valid)
            {
                await _mensagemRepository.Adicionar(mensagem);
                await _mensagemRepository.Commit();
                retorno.PreencherRetorno(new MensagemResponse() { Id = mensagem.Id, Texto = mensagem.Texto, UsuarioId = mensagem.UsuarioId, UsuarioDestinoId = mensagem.UsuarioDestinoId, GrupoId = mensagem.GrupoId, Audio = mensagem.Audio, Foto = mensagem.Foto, Status = mensagem.Status, Cadastro = mensagem.Cadastro });
                if (mensagem.UsuarioDestinoId != null && !string.IsNullOrEmpty(usuarioDestino.Token))
                {
                    ExpoService.SendNotification(new List<string>() { usuarioDestino.Token }, usuarioOrigem.Nome, mensagem.Texto);
                }
                else if (mensagem.GrupoId != null)
                {
                    List<string> tokens = new();
                    foreach (Membro membro in grupo.Membros)
                    {
                        if (!string.IsNullOrEmpty(membro.Usuario.Token) && membro.UsuarioId != usuarioOrigem.Id)
                            tokens.Add(membro.Usuario.Token);
                    }
                    if (tokens.Count > 0)
                        ExpoService.SendNotification(tokens, usuarioOrigem.Nome, mensagem.Texto);
                }
            }
            return retorno;
        }

        //EDITAR MENSAGEM
        public async Task<IResultadoCommand> ManipularAsync(EditarMensagemCommand comando)
        {
            ResultadoCommand retorno = new();

            comando.Validate();
            if (comando.Invalid)
            {
                retorno.AddNotifications(comando);
                return retorno;
            }

            if (comando.UsuarioDestinoId > 0)
            {
                Usuario usuarioOrigem = await _usuarioRepository.ObterPorId(comando.UsuarioId);
                if (usuarioOrigem == null)
                {
                    retorno.AddNotification("Mensagem.UsuarioId", "Usuario não encontrado");
                    return retorno;
                }

                Usuario usuarioDestino = await _usuarioRepository.ObterPorId(comando.UsuarioDestinoId.Value);
                if (usuarioDestino == null)
                {
                    retorno.AddNotification("usuarioDestino.UsuarioId", "Usuario não encontrado");
                    return retorno;
                }
            }
            else if (comando.GrupoId > 0)
            {
                Grupo grupo = await _grupoRepository.ObterPorId(comando.GrupoId.Value);

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
            mensagem.Validate();
            retorno.AddNotifications(mensagem);

            if (retorno.Valid)
            {
                _mensagemRepository.Atualizar(mensagem);
                await _mensagemRepository.Commit();
                retorno.PreencherRetorno(new MensagemResponse() { Id = mensagem.Id, Texto = mensagem.Texto, UsuarioId = mensagem.UsuarioId, UsuarioDestinoId = mensagem.UsuarioDestinoId, GrupoId = mensagem.GrupoId, Audio = mensagem.Audio, Foto = mensagem.Foto, Status = mensagem.Status, Cadastro = mensagem.Cadastro });
            }

            return retorno;
        }

        //LER MENSAGEM
        public async Task<IResultadoCommand> ManipularAsync(LerMensagensCommand comando)
        {
            ResultadoCommand retorno = new();

            comando.Validate();
            if (comando.Invalid)
            {
                retorno.AddNotifications(comando);
                return retorno;
            }

            foreach (long id in comando.Ids)
            {
                Mensagem mensagem = await _mensagemRepository.ObterPorId(id);

                if (mensagem == null)
                {
                    retorno.AddNotification("Mensagem.Id", "Mensagem não encontrado");
                    return retorno;
                }

                if (mensagem.Status != EStatusMensagem.Lida)
                {
                    mensagem.Ler();
                    mensagem.Validate();
                    retorno.AddNotifications(mensagem);

                    if (retorno.Valid)
                    {
                        _mensagemRepository.Atualizar(mensagem);
                        await _mensagemRepository.Commit();
                        retorno.PreencherRetorno(new MensagemResponse() { Id = mensagem.Id, Texto = mensagem.Texto, UsuarioId = mensagem.UsuarioId, UsuarioDestinoId = mensagem.UsuarioDestinoId, GrupoId = mensagem.GrupoId, Audio = mensagem.Audio, Foto = mensagem.Foto, Status = mensagem.Status, Cadastro = mensagem.Cadastro });
                    }
                }
            }

            return retorno;
        }

        //EXCLUIR MENSAGEM
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


        //NOVO LISTA MENSAGEM
        public async Task<IResultadoCommand> ManipularAsync(NovoListaMensagemCommand comando)
        {
            ResultadoCommand retorno = new();

            comando.Validate();
            if (comando.Invalid)
            {
                retorno.AddNotifications(comando);
                return retorno;
            }

            List<Mensagem> mensagens = new();
            foreach (NovoMensagemCommand mensagemCommand in comando.Mensagens)
            {
                List<string> tokens = new();

                Usuario usuarioOrigem = await _usuarioRepository.ObterPorId(mensagemCommand.UsuarioId);
                if (usuarioOrigem == null)
                {
                    retorno.AddNotification("Mensagem.UsuarioId", "Usuario não encontrado");
                    return retorno;
                }

                Usuario usuarioDestino = null;
                Grupo grupo = null;
                if (mensagemCommand.UsuarioDestinoId != null && mensagemCommand.UsuarioDestinoId > 0)
                {
                    usuarioDestino = await _usuarioRepository.ObterPorId(mensagemCommand.UsuarioDestinoId.Value);
                    if (usuarioDestino == null)
                    {
                        retorno.AddNotification("Mensagem.UsuarioDestinoId", "Usuario não encontrado");
                        return retorno;
                    }
                }
                else if (mensagemCommand.GrupoId != null && mensagemCommand.GrupoId > 0)
                {
                    grupo = await _grupoRepository.ObterPorId(mensagemCommand.GrupoId.Value);

                    if (grupo == null)
                    {
                        retorno.AddNotification("Mensagem.GrupoId", "Grupo não encontrado");
                        return retorno;
                    }
                }

                Mensagem mensagem = new(mensagemCommand.Texto, mensagemCommand.UsuarioId, mensagemCommand.UsuarioDestinoId != null && mensagemCommand.UsuarioDestinoId > 0 ? mensagemCommand.UsuarioDestinoId : null, mensagemCommand.GrupoId != null && mensagemCommand.GrupoId > 0 ? mensagemCommand.GrupoId : null, mensagemCommand.Audio, mensagemCommand.Foto, EStatusMensagem.Enviado);
                retorno.AddNotifications(mensagem);
                mensagens.Add(mensagem);
                if (mensagem.UsuarioDestinoId != null && !string.IsNullOrEmpty(usuarioDestino.Token))
                {
                    ExpoService.SendNotification(new List<string>() { usuarioDestino.Token }, usuarioOrigem.Nome, mensagem.Texto);
                }
                else if (mensagem.GrupoId != null)
                {
                    foreach (Membro membro in grupo.Membros)
                    {
                        if (!string.IsNullOrEmpty(membro.Usuario.Token) && membro.UsuarioId != usuarioOrigem.Id)
                            tokens.Add(membro.Usuario.Token);
                    }
                    if (tokens.Count > 0)
                        ExpoService.SendNotification(tokens, usuarioOrigem.Nome, mensagem.Texto);
                }
            }

            if (retorno.Valid)
            {
                await _mensagemRepository.AdicionarLista(mensagens);
                await _mensagemRepository.Commit();
            }

            return retorno;
        }

        public IResultadoCommand ManipularAsync(UploadAudioCommand comando)
        {
            ResultadoCommand retorno = new();
            retorno.PreencherRetorno(_fileUpload.UploadBase64Image(comando.Base64, "blobs", "m4a"));
            return retorno;
        }
    }
}
