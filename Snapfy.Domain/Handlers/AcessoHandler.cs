using Shoalace.Domain.Commands;
using Shoalace.Domain.Commands.Acesso;
using Shoalace.Domain.Entities;
using Shoalace.Domain.Interfaces.Commands;
using Shoalace.Domain.Interfaces.Handlers;
using Shoalace.Domain.Interfaces.Repositories;
using System.Threading.Tasks;

namespace Shoalace.Domain.Handlers
{
    public class AcessoHandler : IHandler<GerarAcessoCommand>, IHandler<ChecarAcessoCommand>
    {
        private readonly IAcessoRepository _acessoRepository;

        public AcessoHandler(IAcessoRepository acessoRepository)
        {
            _acessoRepository = acessoRepository;
        }

        public async Task<IResultadoCommand> ManipularAsync(GerarAcessoCommand comando)
        {
            ResultadoCommand retorno = new();

            comando.Validate();
            if (comando.Invalid)
            {
                retorno.AddNotifications(comando);
                return retorno;
            }

            Acesso acesso = new(comando.UsuarioId);

            if (retorno.Valid)
            {
                await _acessoRepository.Adicionar(acesso);
                await _acessoRepository.Commit();
                retorno.PreencherRetorno(acesso);
            }

            return retorno;
        }

        public async Task<IResultadoCommand> ManipularAsync(ChecarAcessoCommand comando)
        {
            ResultadoCommand retorno = new();

            comando.Validate();
            if (comando.Invalid)
            {
                retorno.AddNotifications(comando);
                return retorno;
            }

            Acesso acesso = await _acessoRepository.ObterPorUsuario(comando.UsuarioId);

            if (acesso == null)
                retorno.AddNotification("Acesso.Codigo", "Acesso não encontrado");

            if (!acesso.Checar(comando.Codigo))
                retorno.AddNotification("Acesso.Codigo", "Código inválido");

            if (retorno.Valid)
            {
                _acessoRepository.Remover(acesso);
                await _acessoRepository.Commit();
                retorno.PreencherRetorno(acesso);
            }

            return retorno;
        }

        public async Task<IResultadoCommand> ManipularAsync(ChecarTokenCommand comando)
        {
            ResultadoCommand retorno = new();

            comando.Validate();
            if (comando.Invalid)
            {
                retorno.AddNotifications(comando);
                return retorno;
            }

            if (retorno.Valid)
            {
                retorno.PreencherRetorno(new{ token="ABCDEF"});
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

            Acesso acesso = await _acessoRepository.ObterPorId(comando.Id);

            if (acesso == null)
            {
                retorno.AddNotification("Acesso.Id", "Acesso não encontrado");
                return retorno;
            }

            if (retorno.Valid)
            {
                _acessoRepository.Remover(acesso);
                await _acessoRepository.Commit();
                retorno.PreencherRetorno(acesso);
            }

            return retorno;
        }
    }
}
