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
        public UsuarioController(IUsuarioRepository usuarioRepository, IGrupoRepository grupoRepository, IMensagemRepository mensagemRepository, IEventoRepository eventoRepository, UsuarioHandler usuarioHandler)
        {
            _usuarioRepository = usuarioRepository;
            _grupoRepository = grupoRepository;
            _mensagemRepository = mensagemRepository;
            _eventoRepository = eventoRepository;
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
            if ((contatos != null) && (contatos.Count > 0))
            {
                foreach (Usuario usuario in contatos)
                {
                    Mensagem mensagem = await _mensagemRepository.ObterUltimaMensagem(id, usuario.Id, false);
                    if (mensagem != null)
                    {
                        contatosHome.Add(
                           new ContatosHome(usuario.Id, usuario.Nome, usuario.Foto, false,
                               string.IsNullOrEmpty(mensagem.Texto) ? (mensagem.Audio != "" ? "Mensagem de áudio" : "Mensagem de mídia") : mensagem.Texto,
                               mensagem.Status,
                               mensagem.Cadastro,
                               mensagem.UsuarioId != id && mensagem.Status != EStatus.Lida ? 1 : 0,
                               mensagem.UsuarioId,
                               new MensagemResponse(mensagem.Id, mensagem.Texto, mensagem.UsuarioId, mensagem.UsuarioDestinoId, mensagem.GrupoId, mensagem.Audio, mensagem.Foto, mensagem.Status, mensagem.Cadastro)
                           )
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
                    contatosHome.Add(
                       new ContatosHome(grupo.Id, grupo.Nome, grupo.Foto, true,
                           mensagem != null ? string.IsNullOrEmpty(mensagem.Texto) ? (mensagem.Audio != "" ? "Mensagem de áudio" : "Mensagem de mídia") : mensagem.Texto : "Grupo novo",
                           mensagem != null ? mensagem.Status : EStatus.Entregue,
                           mensagem != null ? mensagem.Cadastro : grupo.Cadastro,
                           mensagem != null && mensagem.UsuarioId != id && mensagem.Status != EStatus.Lida ? 1 : 0,
                           mensagem != null ? mensagem.UsuarioId : 0,
                           null
                       )
                    );
                }
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
        public async Task<IActionResult> ObterUsuarioPorNumero(long numero) =>
            RetornoController(
                new ResultadoCommand(
                    await _usuarioRepository.ObterPorNumero(numero)
                )
            );

        /// <summary>
        /// Pegar um Usuario com mensagem pelo Id
        /// </summary>
        /// <param name="usuarioId">Id do usuario</param>
        /// /// <param name="contatoId">Id do usuario</param>
        /// <returns>Retorna um usuario</returns>
        [HttpGet("mensagens/{usuarioId:long}/{contatoId:long}")]
        public async Task<IActionResult> ObterUsuarioComMensagem(long usuarioId, long contatoId)
        {
            ContatoChatResponse contatoChat = await _usuarioRepository.ObterContatoChatPorId(contatoId);
            if (contatoChat != null)
            {
                contatoChat.Mensagens = (await _mensagemRepository.ObterTodosPorUsuario(usuarioId, contatoId)).Select(m => new MensagemResponse(m.Id, m.Texto, m.UsuarioId, m.UsuarioDestinoId, m.GrupoId, m.Audio, m.Foto, m.Status, m.Cadastro)).ToList();
                contatoChat.Eventos = (await _eventoRepository.ObterPor2Usuarios(usuarioId, contatoId)).Select(e => new EventoResponse(e.Id, e.Titulo, e.Descricao, e.Cidade, e.Local, e.Valor, e.Latitude, e.Longitude, e.Data, e.Hora, e.Tipo, e.GrupoId, e.Foto, e.Categoria,
               e.MembrosEvento.Select(m => new MembroEventoResponse(m.Id, m.EventoId, m.UsuarioId, m.Comparecer, m.Admin, new UsuarioResponse(m.UsuarioId, m.Usuario.Numero, m.Usuario.Aniversario, m.Usuario.Sexo, m.Usuario.Foto, m.Usuario.Nome, m.Usuario.Bio, m.Usuario.Visto, m.Usuario.Online))).ToList())).ToList();
            }
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
