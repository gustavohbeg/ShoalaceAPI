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
    [Route("Contatos")]
    public class ContatoController : BaseController
    {
        private readonly IContatoRepository _contatoRepository;
        private readonly ContatoHandler _contatoHandler;
        public ContatoController(IContatoRepository contatoRepository, ContatoHandler contatoHandler)
        {
            _contatoRepository = contatoRepository;
            _contatoHandler = contatoHandler;
        }

        /// <summary>
        /// Retorna todos os Contatos
        /// </summary>
        /// <returns>Retorna uma lista de usuario</returns>
        [HttpGet]
        public async Task<IActionResult> ObterUsuarios() =>
            RetornoController(
                new ResultadoCommand(
                    await _contatoRepository.ObterTodos()
                )
            );

        /// <summary>
        /// Pegar todos os contatos por Usuario
        /// </summary>
        /// <param name="id">Id do usuario logado</param>
        /// <returns>Retorna todos os contatos</returns>
        [HttpGet("contatos/{id:long}")]
        public async Task<IActionResult> ObterContatos(long id) =>
            RetornoController(
                new ResultadoCommand(
                    await _contatoRepository.ObterContatosPorUsuario(id)
                )
            );

        /// <summary>
        /// Salva uma lista de contatos
        /// </summary>
        /// <param name="comando">Dados do contato</param>
        /// <returns>Retorna os contatos inseridos</returns>
        [HttpPost]
        public async Task<IActionResult> SalvarUsuario([FromBody] NovoListaContatoCommand comando) =>
            RetornoController(await _contatoHandler.ManipularAsync(comando));

        /// <summary>
        /// Deleta um contato
        /// </summary>
        /// <param name="id">Id do contato</param>
        /// <returns>Retorna se a operação deu sucesso ou não</returns>
        [HttpDelete("{id:long}")]
        public async Task<IActionResult> ExcluirContato(long id) =>
            RetornoController(await _contatoHandler.ManipularAsync(
                new ExcluirCommand()
                {
                    Id = id,
                }
            ));

    }
}
