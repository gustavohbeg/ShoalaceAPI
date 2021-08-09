using Microsoft.AspNetCore.Mvc;
using Shoalace.Domain.Commands;
using Shoalace.Domain.Commands.Grupo;
using Shoalace.Domain.Handlers;
using Shoalace.Domain.Interfaces.Repositories;
using System.Threading.Tasks;

namespace Shoalace.API.Controllers
{
    [ApiController]
    [Route("Grupos")]
    public class GrupoController : BaseController
    {
        private readonly IGrupoRepository _grupoRepository;
        private readonly GrupoHandler _grupoHandler;
        public GrupoController(IGrupoRepository grupoRepository, GrupoHandler grupoHandler)
        {
            _grupoRepository = grupoRepository;
            _grupoHandler = grupoHandler;
        }

        /// <summary>
        /// Retorna todos os grupos
        /// </summary>
        /// <returns>Retorna uma lista de grupos</returns>
        [HttpGet]
        public async Task<IActionResult> ObterGrupos() =>
            RetornoController(
                new ResultadoCommand(
                    await _grupoRepository.ObterTodos()
                )
            );

        /// <summary>
        /// Pegar todos os grupos por usuario
        /// </summary>
        /// <param name="usuarioId">Id do usuario</param>
        /// <returns>Retorna um grupo</returns>
        [HttpGet("{usuarioId:int}")]
        public async Task<IActionResult> ObterTodosPorUsuario(long usuarioId) =>
            RetornoController(
                new ResultadoCommand(
                    await _grupoRepository.ObterTodosPorUsuario(usuarioId)
                )
            );

        /// <summary>
        /// Pegar um grupo pelo Id
        /// </summary>
        /// <param name="id">Id do grupo</param>
        /// <returns>Retorna um grupo</returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObterGrupo(long id) =>
            RetornoController(
                new ResultadoCommand(
                    await _grupoRepository.ObterPorId(id)
                )
            );

        /// <summary>
        /// Salva um novo grupo
        /// </summary>
        /// <param name="comando">Dados do grupo</param>
        /// <returns>Retorna o grupo inserido</returns>
        [HttpPost]
        public async Task<IActionResult> SalvarGrupo([FromBody] NovoGrupoCommand comando) =>
            RetornoController(await _grupoHandler.ManipularAsync(comando));

        /// <summary>
        /// Salva uma lista de grupos
        /// </summary>
        /// <param name="comando">Dados dos grupos</param>
        /// <returns>Retorna o grupo inserido</returns>
        [HttpPost("lista")]
        public async Task<IActionResult> SalvarListaGrupos([FromBody] NovoListaGrupoCommand comando) =>
            RetornoController(await _grupoHandler.ManipularAsync(comando));

        /// <summary>
        /// Alterar um grupo
        /// </summary>
        /// <param name="comando">grupo</param>
        /// <returns>Retorna o grupo alterado</returns>
        [HttpPut]
        public async Task<IActionResult> AlterarEvento([FromBody] EditarGrupoCommand comando) =>
            RetornoController(await _grupoHandler.ManipularAsync(comando));

        /// <summary>
        /// Deleta um grupo
        /// </summary>
        /// <param name="id">Id do grupo</param>
        /// <returns>Retorna se a operação deu sucesso ou não</returns>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> ExcluirGrupo(long id) =>
            RetornoController(await _grupoHandler.ManipularAsync(
                new ExcluirCommand()
                {
                    Id = id,
                }
            ));

        /// <summary>
        /// Inserir membro
        /// </summary>
        /// <param name="comando">Dados do membro</param>
        /// <returns>Retorna o membro inserido</returns>
        [HttpPost("membros")]
        public async Task<IActionResult> InserirMembro([FromBody] InserirMembroCommand comando) =>
            RetornoController(await _grupoHandler.ManipularAsync(comando));
    }
}
