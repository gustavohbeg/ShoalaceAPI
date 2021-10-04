using Shoalace.Domain.Commands;
using Shoalace.Domain.Commands.Grupo;
using Shoalace.Domain.Commands.Usuario;
using Shoalace.Domain.Entities;
using Shoalace.Domain.Interfaces.Commands;
using Shoalace.Domain.Interfaces.Handlers;
using Shoalace.Domain.Interfaces.Repositories;
using Shoalace.Domain.Interfaces.Services;
using Shoalace.Domain.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shoalace.Domain.Handlers
{
    public class GrupoHandler : IHandler<NovoGrupoCommand>, IHandler<EditarGrupoCommand>
    {
        private readonly IGrupoRepository _grupoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IFileUpload _fileUpload;

        public GrupoHandler(IGrupoRepository grupoRepository, IUsuarioRepository usuarioRepository, IFileUpload fileUpload)
        {
            _grupoRepository = grupoRepository;
            _usuarioRepository = usuarioRepository;
            _fileUpload = fileUpload;
        }
        public async Task<IResultadoCommand> ManipularAsync(NovoGrupoCommand comando)
        {
            ResultadoCommand retorno = new();

            comando.Validate();
            if (comando.Invalid)
            {
                retorno.AddNotifications(comando);
                return retorno;
            }

            Grupo grupo = new(comando.Nome, comando.Foto);
            retorno.AddNotifications(grupo);

            List<string> tokens = new();
            foreach (MembroCommand membroCommand in comando.Membros)
            {
                if (!grupo.MembroExiste(membroCommand.UsuarioId))
                {
                    Usuario usuario = await _usuarioRepository.ObterPorId(membroCommand.UsuarioId);
                    if (usuario == null)
                    {
                        retorno.AddNotification("Membro.UsuarioId", "Usuario não encontrado");
                        return retorno;
                    }
                    grupo.AdicionarMembro(new(membroCommand.UsuarioId, 0, membroCommand.Admin));

                    if (!string.IsNullOrEmpty(usuario.Token))
                        tokens.Add(usuario.Token);
                }
            }

            if (retorno.Valid)
            {
                await _grupoRepository.Adicionar(grupo);
                await _grupoRepository.Commit();
                retorno.PreencherRetorno(grupo);
                if (tokens.Count > 0)
                    ExpoService.SendNotification(tokens, grupo.Nome, "Você foi adicionado no grupo");
            }

            return retorno;
        }

        public async Task<IResultadoCommand> ManipularAsync(EditarGrupoCommand comando)
        {
            ResultadoCommand retorno = new();

            comando.Validate();
            if (comando.Invalid)
            {
                retorno.AddNotifications(comando);
                return retorno;
            }

            Grupo grupo = await _grupoRepository.ObterPorId(comando.Id);

            if (grupo == null)
            {
                retorno.AddNotification("Grupo.Id", "Grupo não encontrado");
                return retorno;
            }

            List<string> tokens = new();
            foreach (MembroCommand membroCommand in comando.Membros)
            {
                if (!grupo.MembroExiste(membroCommand.UsuarioId))
                {
                    Usuario usuario = await _usuarioRepository.ObterPorId(membroCommand.UsuarioId);
                    if (usuario == null)
                    {
                        retorno.AddNotification("Membro.UsuarioId", "Usuario não encontrado");
                        return retorno;
                    }
                    grupo.AdicionarMembro(new(membroCommand.UsuarioId, 0, membroCommand.Admin));

                    if (!string.IsNullOrEmpty(usuario.Token))
                        tokens.Add(usuario.Token);
                }
            }

            grupo.PreencherGrupo(comando.Nome, comando.Foto);
            retorno.AddNotifications(grupo);

            if (retorno.Valid)
            {
                _grupoRepository.Atualizar(grupo);
                await _grupoRepository.Commit();
                retorno.PreencherRetorno(grupo);
                if (tokens.Count > 0)
                    ExpoService.SendNotification(tokens, grupo.Nome, "Você foi adicionado no grupo");
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

            Grupo grupo = await _grupoRepository.ObterPorId(comando.Id);

            if (grupo == null)
            {
                retorno.AddNotification("Grupo.Id", "Grupo não encontrado");
                return retorno;
            }

            if (retorno.Valid)
            {
                _grupoRepository.Remover(grupo);
                await _grupoRepository.Commit();
                retorno.PreencherRetorno(grupo);
            }

            return retorno;
        }

        public async Task<IResultadoCommand> ManipularAsync(NovoListaGrupoCommand comando)
        {
            ResultadoCommand retorno = new();

            comando.Validate();
            if (comando.Invalid)
            {
                retorno.AddNotifications(comando);
                return retorno;
            }

            Grupo grupo;
            List<Grupo> grupos = new();
            foreach (NovoGrupoCommand grupoCommand in comando.Grupos)
            {
                grupo = new(grupoCommand.Nome, grupoCommand.Foto);
                retorno.AddNotifications(grupo);
                grupos.Add(grupo);
            }

            if (retorno.Valid)
            {
                await _grupoRepository.AdicionarLista(grupos);
                await _grupoRepository.Commit();
            }

            return retorno;
        }

        public async Task<IResultadoCommand> ManipularAsync(NovoMembroCommand comando)
        {
            ResultadoCommand retorno = new();

            comando.Validate();
            if (comando.Invalid)
            {
                retorno.AddNotifications(comando);
                return retorno;
            }

            Grupo grupo = await _grupoRepository.ObterPorId(comando.GrupoId);

            if (grupo == null)
            {
                retorno.AddNotification("Grupo.Id", "Grupo não encontrado");
                return retorno;
            }

            Membro membro = new(comando.UsuarioId, comando.GrupoId, comando.Admin);

            grupo.AdicionarMembro(membro);

            if (retorno.Valid)
            {
                _grupoRepository.Atualizar(grupo);
                await _grupoRepository.Commit();
                retorno.PreencherRetorno(grupo);
            }

            return retorno;
        }

        public async Task<IResultadoCommand> ManipularAsync(UploadImageCommand comando)
        {
            ResultadoCommand retorno = new();
            retorno.PreencherRetorno(_fileUpload.UploadBase64Image(comando.Base64, "blobs"));
            return retorno;
        }
    }
}
