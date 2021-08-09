using Microsoft.AspNetCore.Mvc;
using Shoalace.Domain.Commands;
using Shoalace.Domain.Commands.Usuario;
using Shoalace.Domain.Handlers;
using Shoalace.Domain.Interfaces.Repositories;
using System.Threading.Tasks;

namespace Shoalace.API.Controllers
{
    [ApiController]
    [Route("Usuarios")]
    public class UsuarioController : BaseController
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly UsuarioHandler _usuarioHandler;
        public UsuarioController(IUsuarioRepository usuarioRepository, UsuarioHandler usuarioHandler)
        {
            _usuarioRepository = usuarioRepository;
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
        [HttpGet("contatos/{id:int}")]
        public async Task<IActionResult> ObterContatos(long id) =>
            RetornoController(
                new ResultadoCommand(
                    await _usuarioRepository.ObterContatos(id)
                )
            );

        /// <summary>
        /// Pegar um Usuario pelo Id
        /// </summary>
        /// <param name="id">Id do usuario</param>
        /// <returns>Retorna um usuario</returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObterUsuario(long id) =>
            RetornoController(
                new ResultadoCommand(
                    await _usuarioRepository.ObterPorId(id)
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
