using Microsoft.AspNetCore.Mvc;
using Shoalace.Domain.Commands;
using Shoalace.Domain.Commands.Acesso;
using Shoalace.Domain.Handlers;
using Shoalace.Domain.Interfaces.Repositories;
using System.Threading.Tasks;

namespace Shoalace.API.Controllers
{
    [ApiController]
    [Route("Acessos")]
    public class AcessoController : BaseController
    {
        private readonly IAcessoRepository _acessoRepository;
        private readonly AcessoHandler _acessoHandler;
        public AcessoController(IAcessoRepository acessoRepository, AcessoHandler acessoHandler)
        {
            _acessoRepository = acessoRepository;
            _acessoHandler = acessoHandler;
        }

        /// <summary>
        /// Retorna todos os acessos
        /// </summary>
        /// <returns>Retorna uma lista de acessos</returns>
        [HttpGet]
        public async Task<IActionResult> ObterAcessos() =>
            RetornoController(
                new ResultadoCommand(
                    await _acessoRepository.ObterTodos()
                )
            );

        /// <summary>
        /// Pegar um acesso pelo Id
        /// </summary>
        /// <param name="id">Id do acesso</param>
        /// <returns>Retorna um acesso</returns>
        [HttpGet("{id:long}")]
        public async Task<IActionResult> ObterAcesso(long id) =>
            RetornoController(
                new ResultadoCommand(
                    await _acessoRepository.ObterPorId(id)
                )
            );

        /// <summary>
        /// Gera um novo acesso
        /// </summary>
        /// <param name="comando">Dados do acesso</param>
        /// <returns>Retorna o acesso inserido</returns>
        [HttpPost]
        public async Task<IActionResult> GerarAcesso([FromBody] GerarAcessoCommand comando) =>
            RetornoController(await _acessoHandler.ManipularAsync(comando));

        /// <summary>
        /// Checa codigo acesso
        /// </summary>
        /// <param name="comando">Dados do acesso</param>
        /// <returns>Retorna se deu sucesso</returns>
        [HttpPost("ChecarAcesso")]
        public async Task<IActionResult> ChecarAcesso([FromBody] ChecarAcessoCommand comando) =>
            RetornoController(await _acessoHandler.ManipularAsync(comando));

        /// <summary>
        /// Checar token
        /// </summary>
        /// <param name="comando">Dados do acesso</param>
        /// <returns>Retorna se deu sucesso</returns>
        [HttpPost("ChecarToken")]
        public async Task<IActionResult> ChecarToken([FromBody] ChecarTokenCommand comando) =>
            RetornoController(await _acessoHandler.ManipularAsync(comando));

        /// <summary>
        /// Alterar um acesso
        /// </summary>
        /// <param name="comando">acesso</param>
        /// <returns>Retorna o acesso alterado</returns>
        [HttpPut]
        public async Task<IActionResult> AlterarAcesso([FromBody] ExcluirCommand comando) =>
            RetornoController(await _acessoHandler.ManipularAsync(comando));

        /// <summary>
        /// Deleta um acesso
        /// </summary>
        /// <param name="id">Id do acesso</param>
        /// <returns>Retorna se a operação deu sucesso ou não</returns>
        [HttpDelete("{id:long}")]
        public async Task<IActionResult> ExcluirAcesso(long id) =>
            RetornoController(await _acessoHandler.ManipularAsync(
                new ExcluirCommand()
                {
                    Id = id,
                }
            ));

    }
}
