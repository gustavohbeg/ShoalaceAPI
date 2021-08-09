using Shoalace.Domain.Commands;
using Shoalace.Domain.Commands.Usuario;
using Shoalace.Domain.Entities;
using Shoalace.Domain.Interfaces.Commands;
using Shoalace.Domain.Interfaces.Handlers;
using Shoalace.Domain.Interfaces.Repositories;
using System.Threading.Tasks;

namespace Shoalace.Domain.Handlers
{
    public class UsuarioHandler : IHandler<NovoUsuarioCommand>, IHandler<EditarUsuarioCommand>
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioHandler(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        public async Task<IResultadoCommand> ManipularAsync(NovoUsuarioCommand comando)
        {
            ResultadoCommand retorno = new();

            comando.Validate();
            if (comando.Invalid)
            {
                retorno.AddNotifications(comando);
                return retorno;
            }

            Usuario usuario = new(comando.Aniversario, comando.Sexo, comando.Foto, comando.Nome, comando.Bio, comando.Visto, comando.Latitude, comando.Longitude, comando.Token);

            if (retorno.Valid)
            {
                await _usuarioRepository.Adicionar(usuario);
                await _usuarioRepository.Commit();
                retorno.PreencherRetorno(usuario);
            }

            return retorno;
        }

        public async Task<IResultadoCommand> ManipularAsync(EditarUsuarioCommand comando)
        {
            ResultadoCommand retorno = new();

            comando.Validate();
            if (comando.Invalid)
            {
                retorno.AddNotifications(comando);
                return retorno;
            }

            Usuario usuario = await _usuarioRepository.ObterPorId(comando.Id);

            if (usuario == null)
            {
                retorno.AddNotification("Usuario.Id", "Usuario não encontrado");
                return retorno;
            }

            usuario.PreencherUsuario(comando.Aniversario, comando.Sexo, comando.Foto, comando.Nome, comando.Bio, comando.Visto, comando.Latitude, comando.Longitude, comando.Token);

            if (retorno.Valid)
            {
                _usuarioRepository.Atualizar(usuario);
                await _usuarioRepository.Commit();
                retorno.PreencherRetorno(usuario);
            }

            return retorno;
        }

        public async Task<IResultadoCommand> ManipularAsync(AtualizarVistoCommand comando)
        {
            ResultadoCommand retorno = new();

            comando.Validate();
            if (comando.Invalid)
            {
                retorno.AddNotifications(comando);
                return retorno;
            }

            Usuario usuario = await _usuarioRepository.ObterPorId(comando.Id);

            if (usuario == null)
            {
                retorno.AddNotification("Usuario.Id", "Usuario não encontrado");
                return retorno;
            }

            usuario.AtualizarVisto();

            if (retorno.Valid)
            {
                _usuarioRepository.Atualizar(usuario);
                await _usuarioRepository.Commit();
                retorno.PreencherRetorno(usuario);
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

            Usuario usuario = await _usuarioRepository.ObterPorId(comando.Id);

            if (usuario == null)
            {
                retorno.AddNotification("Usuario.Id", "Usuario não encontrado");
                return retorno;
            }

            if (retorno.Valid)
            {
                _usuarioRepository.Remover(usuario);
                await _usuarioRepository.Commit();
                retorno.PreencherRetorno(usuario);
            }

            return retorno;
        }
    }
}
