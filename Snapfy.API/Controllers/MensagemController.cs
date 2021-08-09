using Microsoft.AspNetCore.Mvc;
using Shoalace.Domain.Commands;
using Shoalace.Domain.Commands.Mensagem;
using Shoalace.Domain.Handlers;
using Shoalace.Domain.Interfaces.Repositories;
using System.Threading.Tasks;

namespace Shoalace.API.Controllers
{
    [ApiController]
    [Route("Mensagens")]
    public class MensagemController : BaseController
    {
        private readonly IMensagemRepository _mensagemRepository;
        private readonly MensagemHandler _mensagemHandler;
        public MensagemController(IMensagemRepository mensagemRepository, MensagemHandler mensagemHandler)
        {
            _mensagemRepository = mensagemRepository;
            _mensagemHandler = mensagemHandler;
        }

        /// <summary>
        /// Retorna todos as mensagens
        /// </summary>
        /// <returns>Retorna uma lista de mensagens</returns>
        [HttpGet]
        public async Task<IActionResult> ObterMensagens() =>
            RetornoController(
                new ResultadoCommand(
                    await _mensagemRepository.ObterTodos()
                )
            );

        /// <summary>
        /// Retorna mensagens pendentes por usuario
        /// </summary>
        /// <param name="usuarioId">Id do usuario</param>
        /// <returns>Retorna uma lista de mensagens</returns>
        [HttpGet("pendentes/{usuarioId:int}")]
        public async Task<IActionResult> ObterMensagensPendentes(int usuarioId) =>
            RetornoController(
                new ResultadoCommand(
                    await _mensagemRepository.ObterPendentesPorUsuario(usuarioId)
                )
            );

        /// <summary>
        /// Pegar uma mensagem pelo Id
        /// </summary>
        /// <param name="id">Id da mensagem</param>
        /// <returns>Retorna uma menssagem</returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObterMensagem(int id) =>
            RetornoController(
                new ResultadoCommand(
                    await _mensagemRepository.ObterPorId(id)
                )
            );

        /// <summary>
        /// Salva uma nova mensagem
        /// </summary>
        /// <param name="comando">Dados da mensagem</param>
        /// <returns>Retorna a meensagem inserida</returns>
        [HttpPost]
        public async Task<IActionResult> SalvarMensagem([FromBody] NovoMensagemCommand comando) =>
            RetornoController(await _mensagemHandler.ManipularAsync(comando));

        /// <summary>
        /// Salva uma lista de mensagens
        /// </summary>
        /// <param name="comando">Dados das mensagens</param>
        /// <returns>Retorna as meensagens inseridas</returns>
        [HttpPost("lista")]
        public async Task<IActionResult> SalvarListaMensagens([FromBody] NovoListaMensagemCommand comando) =>
            RetornoController(await _mensagemHandler.ManipularAsync(comando));

        /// <summary>
        /// Alterar uma mensagem
        /// </summary>
        /// <param name="comando">mensagem</param>
        /// <returns>Retorna a mensagem alterada</returns>
        [HttpPut]
        public async Task<IActionResult> AlterarMensagem([FromBody] EditarMensagemCommand comando) =>
            RetornoController(await _mensagemHandler.ManipularAsync(comando));

        /// <summary>
        /// Deleta uma mensagem
        /// </summary>
        /// <param name="id">Id da mensagem</param>
        /// <returns>Retorna se a operação deu sucesso ou não</returns>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> ExcluirMensagem(int id) =>
            RetornoController(await _mensagemHandler.ManipularAsync(
                new ExcluirCommand()
                {
                    Id = id,
                }
            ));

    }
}
