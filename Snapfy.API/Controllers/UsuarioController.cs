using Microsoft.AspNetCore.Mvc;
using Shoalace.Domain.Commands;
using Shoalace.Domain.Commands.Usuario;
using Shoalace.Domain.Entities;
using Shoalace.Domain.Handlers;
using Shoalace.Domain.Interfaces.Repositories;
using Shoalace.Domain.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Shoalace.Domain.Enums;
using Shoalace.Domain.Commands.Mensagem;

namespace Shoalace.API.Controllers
{
    [ApiController]
    [Route("Usuarios")]
    public class UsuarioController : BaseController
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IGrupoRepository _grupoRepository;
        private readonly IMensagemRepository _mensagemRepository;
        private readonly IEventoRepository _eventoRepository;
        private readonly UsuarioHandler _usuarioHandler;
        private readonly MensagemHandler _mensagemHandler;
        public UsuarioController(IUsuarioRepository usuarioRepository, IGrupoRepository grupoRepository, IMensagemRepository mensagemRepository, IEventoRepository eventoRepository, UsuarioHandler usuarioHandler, MensagemHandler mensagemHandler)
        {
            _usuarioRepository = usuarioRepository;
            _grupoRepository = grupoRepository;
            _mensagemRepository = mensagemRepository;
            _eventoRepository = eventoRepository;
            _usuarioHandler = usuarioHandler;
            _mensagemHandler = mensagemHandler;
        }

        /// <summary>
        /// Retorna todos os Usuarios
        /// </summary>
        /// <returns>Retorna uma lista de usuario</returns>
        [HttpGet]
        public async Task<IActionResult> ObterUsuarios() =>
            RetornoController(
                new ResultadoCommand(
                    await _usuarioRepository.ObterTodos()
                )
            );

        /// <summary>
        /// Pegar todos os contatos
        /// </summary>
        /// <param name="id">Id do usuario logado</param>
        /// <returns>Retorna todos os contatos</returns>
        [HttpGet("contatos/{id:long}")]
        public async Task<IActionResult> ObterContatos(long id) =>
            RetornoController(
                new ResultadoCommand(
                    await _usuarioRepository.ObterContatos(id)
                )
            );

        /// <summary>
        /// Pegar todos os contatos com ultima mensagem
        /// </summary>
        /// <param name="id">Id do usuario</param>
        /// <returns>Retorna todos os contatos</returns>
        [HttpGet("contatosHome/{id:long}")]
        public async Task<IActionResult> ObterContatosHome(long id)
        {
            List<ContatosHome> contatosHome = new();

            List<Usuario> contatos = await _usuarioRepository.ObterContatos(id);
            foreach (Usuario usuario in contatos)
            {
                MensagemResponse mensagem = await _mensagemRepository.ObterUltimaMensagemResponse(id, usuario.Id, false);
                if (mensagem != null)
                {
                    contatosHome.Add(
                       new ContatosHome()
                       { 
                           Id = usuario.Id,
                           Nome = usuario.Nome,
                           Foto = usuario.Foto,
                           IsGrupo = false,
                           Texto = string.IsNullOrEmpty(mensagem.Texto) ? (mensagem.Audio != "" ? "Mensagem de áudio" : "Mensagem de mídia") : mensagem.Texto,
                           Status = mensagem.Status,
                           Cadastro = mensagem.Cadastro,
                           NaoLidas = (await _mensagemRepository.ObterNaoLidasPorContato(id, usuario.Id)).Count,
                           UsuarioId = mensagem.UsuarioId,
                           UltimaMensagem = null
                       }
                    );
                }
            }

            List<Grupo> grupos = await _grupoRepository.ObterTodosPorUsuario(id);
            foreach (Grupo grupo in grupos)
            {
                MensagemResponse mensagem = await _mensagemRepository.ObterUltimaMensagemResponse(id, grupo.Id, false);
                contatosHome.Add(
                   new ContatosHome()
                   { 
                       Id = grupo.Id,
                       Nome = grupo.Nome,
                       Foto = grupo.Foto,
                       IsGrupo = true,
                       Texto = mensagem != null ? string.IsNullOrEmpty(mensagem.Texto) ? (mensagem.Audio != "" ? "Mensagem de áudio" : "Mensagem de mídia") : mensagem.Texto : "Grupo novo",
                       Status = mensagem?.Status ?? EStatusMensagem.Entregue,
                       Cadastro = mensagem?.Cadastro ?? grupo.Cadastro,
                       NaoLidas = (await _mensagemRepository.ObterNaoLidasPorContato(id, grupo.Id)).Count,
                       UsuarioId = mensagem?.UsuarioId ?? 0,
                       UltimaMensagem = null
                   }
                );
            }

            return RetornoController(
                new ResultadoCommand(
                    contatosHome.OrderByDescending(c => c.Cadastro)
                )
            );
        }

        /// <summary>
        /// Pegar um Usuario pelo Id
        /// </summary>
        /// <param name="usuarioId">Id do usuario</param>
        /// <returns>Retorna um usuario</returns>
        [HttpGet("{id:long}")]
        public async Task<IActionResult> ObterUsuario(long id) =>
            RetornoController(
                new ResultadoCommand(
                    await _usuarioRepository.ObterPorId(id)
                )
            );

        /// <summary>
        /// Pegar um Usuario pelo Numero
        /// </summary>
        /// <param name="numero">Numero do usuario</param>
        /// <returns>Retorna um usuario</returns>
        [HttpGet("numero/{numero:long}")]
        public async Task<IActionResult> ObterUsuarioPorNumero(string numero) =>
            RetornoController(
                new ResultadoCommand(
                    await _usuarioRepository.ObterPorNumero(numero)
                )
            );

        /// <summary>
        /// Pegar um Usuario com mensagens pelo Id (e ler a mensagens)
        /// </summary>
        /// <param name="usuarioId">Id do usuario</param>
        /// /// <param name="contatoId">Id do contato</param>
        /// <returns>Retorna um usuario com mensagens</returns>
        [HttpGet("mensagens/{usuarioId:long}/{contatoId:long}")]
        public async Task<IActionResult> ObterUsuarioComMensagem(long usuarioId, long contatoId)
        {
            ContatoChatResponse contatoChat = await _usuarioRepository.ObterContatoChatPorId(contatoId);
            if (contatoChat != null)
            {
                contatoChat.Mensagens = await _mensagemRepository.ObterTodosResponsePorUsuario(usuarioId, contatoId);
                contatoChat.Eventos = await _eventoRepository.ObterResponsesPor2Usuarios(usuarioId, contatoId);

                List<long> ids = new();
                foreach (MensagemResponse mensagem in contatoChat.Mensagens)
                    if (mensagem.Status != EStatusMensagem.Lida) ids.Add(mensagem.Id);

                if (ids.Count > 0) await _mensagemHandler.ManipularAsync(new LerMensagensCommand() { Ids = ids });
            };
            return RetornoController(
                 new ResultadoCommand(
                     contatoChat
                 )
             );
        }

        /// <summary>
        /// Salva um novo usuario
        /// </summary>
        /// <param name="comando">Dados do usuario</param>
        /// <returns>Retorna o usuario inserido</returns>
        [HttpPost]
        public async Task<IActionResult> SalvarUsuario([FromBody] NovoUsuarioCommand comando) =>
            RetornoController(await _usuarioHandler.ManipularAsync(comando));

        /// <summary>
        /// Salva foto de usuario
        /// </summary>
        /// <param name="comando">Dados do usuario</param>
        /// <returns>Retorna o usuario inserido</returns>
        [HttpPost("image")]
        public async Task<IActionResult> UploadImage([FromBody] UploadImageCommand comando) =>
            RetornoController(_usuarioHandler.Manipular(comando));


        /// <summary>
        /// Atualizar visto
        /// </summary>
        /// <param name="comando"></param>
        /// <returns>Retorna o visto atualizado</returns>
        [HttpPost("visto")]
        public async Task<IActionResult> AtualizarVisto([FromBody] AtualizarVistoCommand comando) =>
            RetornoController(await _usuarioHandler.ManipularAsync(comando));

        /// <summary>
        /// Alterar um usuario
        /// </summary>
        /// <param name="comando">usuario</param>
        /// <returns>Retorna o usuario alterado</returns>
        [HttpPut]
        public async Task<IActionResult> AlterarEmpresa([FromBody] EditarUsuarioCommand comando) =>
            RetornoController(await _usuarioHandler.ManipularAsync(comando));

        /// <summary>
        /// Deleta um usuario
        /// </summary>
        /// <param name="id">Id do usuario</param>
        /// <returns>Retorna se a operação deu sucesso ou não</returns>
        [HttpDelete("{id:long}")]
        public async Task<IActionResult> ExcluirUsuario(long id) =>
            RetornoController(await _usuarioHandler.ManipularAsync(
                new ExcluirCommand()
                {
                    Id = id,
                }
            ));

    }
}
