using Microsoft.AspNetCore.Mvc;
using Shoalace.Domain.Commands;
using Shoalace.Domain.Commands.Evento;
using Shoalace.Domain.Commands.Usuario;
using Shoalace.Domain.Enums;
using Shoalace.Domain.Handlers;
using Shoalace.Domain.Interfaces.Repositories;
using System;
using System.Threading.Tasks;

namespace Shoalace.API.Controllers
{
    [ApiController]
    [Route("Eventos")]
    public class EventoController : BaseController
    {
        private readonly IEventoRepository _eventoRepository;
        private readonly EventoHandler _eventoHandler;
        public EventoController(IEventoRepository eventoRepository, EventoHandler eventoHandler)
        {
            _eventoRepository = eventoRepository;
            _eventoHandler = eventoHandler;
        }

        /// <summary>
        /// Retorna todos os eventos
        /// </summary>
        /// <returns>Retorna uma lista de eventos</returns>
        [HttpGet]
        public async Task<IActionResult> ObterEventos() =>
            RetornoController(
                new ResultadoCommand(
                    await _eventoRepository.ObterTodos()
                )
            );

        /// <summary>
        /// Retorna todos os eventos por usuario
        /// </summary>
        /// <returns>Retorna uma lista de eventos por usuario</returns>
        [HttpGet("usuarios/{usuarioId:long}")]
        public async Task<IActionResult> ObterEventosPorUsuario(long usuarioId) =>
            RetornoController(
                new ResultadoCommand(
                    await _eventoRepository.ObterTodosPorUsuario(usuarioId)
                )
            );

        /// <summary>
        /// Retorna todos os eventos do explorar
        /// </summary>
        /// <returns>Retorna uma lista de eventos a explorar</returns>
        [HttpGet("explorar")]
        public async Task<IActionResult> ObterEventosExplorar() =>
            RetornoController(
                new ResultadoCommand(
                    await _eventoRepository.ObterTodosExplorar()
                )
            );

        /// <summary>
        /// Retorna todos os eventos por data
        /// </summary>
        /// <returns>Retorna uma lista de eventos por data</returns>
        [HttpGet("{data:datetime}")]
        public async Task<IActionResult> ObterEventosPorData(DateTime data) =>
            RetornoController(
                new ResultadoCommand(
                    await _eventoRepository.ObterTodosPorData(data)
                )
            );

        /// <summary>
        /// Retorna os eventos por cidade e categoria
        /// </summary>
        /// <returns>Retorna uma lista de eventos por cidade</returns>
        [HttpGet("{categoria:int}/{cidade}")]
        public async Task<IActionResult> ObterPorCategoriaECidade(ECategoria categoria, string cidade) =>
            RetornoController(
                new ResultadoCommand(
                    await _eventoRepository.ObterPorCategoriaECidade(categoria, cidade)
                )
            );

        /// <summary>
        /// Pegar um evento pelo Id
        /// </summary>
        /// <param name="id">Id do evento</param>
        /// <returns>Retorna um evento</returns>
        [HttpGet("{id:long}")]
        public async Task<IActionResult> ObterEvento(long id) =>
            RetornoController(
                new ResultadoCommand(
                    await _eventoRepository.ObterPorId(id)
                )
            );

        /// <summary>
        /// Salva um novo evento
        /// </summary>
        /// <param name="comando">Dados do evento</param>
        /// <returns>Retorna o evento inserido</returns>
        [HttpPost]
        public async Task<IActionResult> SalvarEvento([FromBody] NovoEventoCommand comando) =>
            RetornoController(await _eventoHandler.ManipularAsync(comando));

        /// <summary>
        /// Salva uma lista de eventos
        /// </summary>
        /// <param name="comando">Dados dos eventos</param>
        /// <returns>Retorna os eventos inseridos</returns>
        [HttpPost("lista")]
        public async Task<IActionResult> SalvarListaEvento([FromBody] NovoListaEventoCommand comando) =>
            RetornoController(await _eventoHandler.ManipularAsync(comando));

        /// <summary>
        /// Alterar um evento
        /// </summary>
        /// <param name="comando">evento</param>
        /// <returns>Retorna o evento alterado</returns>
        [HttpPut]
        public async Task<IActionResult> AlterarEvento([FromBody] EditarEventoCommand comando) =>
            RetornoController(await _eventoHandler.ManipularAsync(comando));

        /// <summary>
        /// Inserir membroEvento
        /// </summary>
        /// <param name="comando">Dados do membroEvento</param>
        /// <returns>Retorna o membroEvento inserido</returns>
        [HttpPost("membros")]
        public async Task<IActionResult> InserirMembro([FromBody] InserirMembroEventoCommand comando) =>
            RetornoController(await _eventoHandler.ManipularAsync(comando));

        /// <summary>
        /// Salva foto de evento
        /// </summary>
        /// <param name="comando">Dados do evento</param>
        /// <returns>Retorna o usuario inserido</returns>
        [HttpPost("image")]
        public async Task<IActionResult> UploadImage([FromBody] UploadImageCommand comando) =>
            RetornoController(await _eventoHandler.ManipularAsync(comando));

        /// <summary>
        /// Alterar membroEvento
        /// </summary>
        /// <param name="comando">Dados do membroEvento</param>
        /// <returns>Retorna o membroEvento inserido</returns>
        [HttpPut("membros")]
        public async Task<IActionResult> InserirMembro([FromBody] EditarMembroEventoCommand comando) =>
            RetornoController(await _eventoHandler.ManipularAsync(comando));

        /// <summary>
        /// Deleta um evento
        /// </summary>
        /// <param name="id">Id do evento</param>
        /// <returns>Retorna se a operação deu sucesso ou não</returns>
        [HttpDelete("{id:long}")]
        public async Task<IActionResult> ExcluirEvento(long id) =>
            RetornoController(await _eventoHandler.ManipularAsync(
                new ExcluirCommand()
                {
                    Id = id,
                }
            ));
    }
}
