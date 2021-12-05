using Shoalace.Domain.Commands;
using Shoalace.Domain.Commands.Usuario;
using Shoalace.Domain.Entities;
using Shoalace.Domain.Interfaces.Commands;
using Shoalace.Domain.Interfaces.Handlers;
using Shoalace.Domain.Interfaces.Repositories;
using Shoalace.Domain.Interfaces.Services;
using Shoalace.Domain.Services;
using System.Threading.Tasks;

namespace Shoalace.Domain.Handlers
{
    public class ContatoHandler : IHandler<NovoListaContatoCommand>
    {
        private readonly IContatoRepository _contatoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IFileUpload _fileUpload;

        public ContatoHandler(IContatoRepository contatoRepository, IUsuarioRepository usuarioRepository, IFileUpload fileUpload)
        {
            _contatoRepository = contatoRepository;
            _usuarioRepository = usuarioRepository;
            _fileUpload = fileUpload;
        }

        //NOVO USUARIO
        public async Task<IResultadoCommand> ManipularAsync(NovoListaContatoCommand comando)
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

            foreach (long contatoId in comando.Contatos)
            {
                Contato contato = new(comando.Id, contatoId);
                retorno.AddNotifications(contato);
                await _contatoRepository.Adicionar(contato);
            }
            
            if (retorno.Valid)
            {
                await _contatoRepository.Commit();
                retorno.PreencherRetorno("Adicionados");
                //ExpoService.SendNotification(usuario.Token, "Cadastro", "Cadastrado com sucesso");
            }

            return retorno;
        }

        //EXCLUIR USUARIO
        public async Task<IResultadoCommand> ManipularAsync(ExcluirCommand comando)
        {
            ResultadoCommand retorno = new();

            comando.Validate();
            if (comando.Invalid)
            {
                retorno.AddNotifications(comando);
                return retorno;
            }

            Contato contato = await _contatoRepository.ObterPorId(comando.Id);

            if (contato == null)
            {
                retorno.AddNotification("Usuario.Id", "Usuario não encontrado");
                return retorno;
            }

            if (retorno.Valid)
            {
                _contatoRepository.Remover(contato);
                await _contatoRepository.Commit();
                retorno.PreencherRetorno(contato);
            }

            return retorno;
        }
    }
}
