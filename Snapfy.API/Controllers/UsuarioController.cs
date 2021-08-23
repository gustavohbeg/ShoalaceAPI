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

namespace Shoalace.API.Controllers
{
    [ApiController]
    [Route("Usuarios")]
    public class UsuarioController : BaseController
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IGrupoRepository _grupoRepository;
        private readonly IMensagemRepository _mensagemRepository;
        private readonly UsuarioHandler _usuarioHandler;
        public UsuarioController(IUsuarioRepository usuarioRepository, IGrupoRepository grupoRepository, IMensagemRepository mensagemRepository, UsuarioHandler usuarioHandler)
        {
            _usuarioRepository = usuarioRepository;
            _grupoRepository = grupoRepository;
            _mensagemRepository = mensagemRepository;
            _usuarioHandler = usuarioHandler;
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
        /// <param name="id">Id do usuario</param>
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
            if ((contatos != null) && (contatos.Count > 0))
            {
                foreach (Usuario usuario in contatos)
                {
                    Mensagem mensagem = await _mensagemRepository.ObterUltimaMensagem(id, usuario.Id, false);
                    if (mensagem != null)
                    {
                        contatosHome.Add(
                           new ContatosHome()
                           {
                               Id = usuario.Id,
                               Nome = usuario.Nome,
                               Foto = usuario.Foto.Value,
                               IsGrupo = false,
                               Texto = string.IsNullOrEmpty(mensagem.Texto) ? (mensagem.Audio.HasValue ? "Mensagem de áudio" : "Mensagem de mídia") : mensagem.Texto,
                               Status = mensagem.Status,
                               Cadastro = mensagem.Cadastro
                           }
                        );
                    }
                }
            }

            List<Grupo> grupos = await _grupoRepository.ObterTodosPorUsuario(id);
            if ((grupos != null) && (grupos.Count > 0))
            {
                foreach (Grupo grupo in grupos)
                {
                    Mensagem mensagem = await _mensagemRepository.ObterUltimaMensagem(id, grupo.Id, true);
                    if (mensagem != null)
                    {
                        contatosHome.Add(
                           new ContatosHome()
                           {
                               Id = grupo.Id,
                               Nome = grupo.Nome,
                               Foto = grupo.Foto.Value,
                               IsGrupo = true,
                               Texto = string.IsNullOrEmpty(mensagem.Texto) ? (mensagem.Audio.HasValue ? "Mensagem de áudio" : "Mensagem de mídia") : mensagem.Texto,
                               Status = mensagem.Status,
                               Cadastro = mensagem.Cadastro
                           }
                        );
                    }
                }
            }

            return RetornoController(
                new ResultadoCommand(
                    contatosHome.OrderBy(c => c.Cadastro)
                )
            );
        }

        /// <summary>
        /// Pegar um Usuario pelo Id
        /// </summary>
        /// <param name="numero">Id do usuario</param>
        /// <returns>Retorna um usuario</returns>
        [HttpGet("{numero:long}")]
        public async Task<IActionResult> ObterUsuario(long numero) =>
            RetornoController(
                new ResultadoCommand(
                    await _usuarioRepository.ObterPorNumero(numero)
                )
            );

        /// <summary>
        /// Salva um novo usuario
        /// </summary>
        /// <param name="comando">Dados do usuario</param>
        /// <returns>Retorna o usuario inserido</returns>
        [HttpPost]
        public async Task<IActionResult> SalvarUsuario([FromBody] NovoUsuarioCommand comando) =>
            RetornoController(await _usuarioHandler.ManipularAsync(comando));

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

    }
}
