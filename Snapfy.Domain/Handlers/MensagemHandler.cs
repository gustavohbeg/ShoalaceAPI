using Shoalace.Domain.Commands;
using Shoalace.Domain.Commands.Mensagem;
using Shoalace.Domain.Entities;
using Shoalace.Domain.Enums;
using Shoalace.Domain.Interfaces.Commands;
using Shoalace.Domain.Interfaces.Handlers;
using Shoalace.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shoalace.Domain.Handlers
{
    public class MensagemHandler : IHandler<NovoMensagemCommand>, IHandler<EditarMensagemCommand>
    {
        private readonly IMensagemRepository _mensagemRepository;

        public MensagemHandler(IMensagemRepository mensagemRepository)
        {
            _mensagemRepository = mensagemRepository;
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

            Mensagem mensagem = new(comando.Texto, comando.UsuarioId, comando.UsuarioDestinoId, comando.GrupoId, comando.Audio, comando.Foto, EStatus.Enviado);

            if (retorno.Valid)
            {
                await _mensagemRepository.Adicionar(mensagem);
                await _mensagemRepository.Commit();
                retorno.PreencherRetorno(mensagem);
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

            Mensagem mensagem = await _mensagemRepository.ObterPorId(comando.Id);

            if (mensagem == null)
            {
                retorno.AddNotification("Mensagem.Id", "Mensagem não encontrado");
                return retorno;
            }

            mensagem.PreencherMensagem(comando.Texto, comando.UsuarioId, comando.UsuarioDestinoId, comando.GrupoId, comando.Audio, comando.Foto, comando.Status);

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
