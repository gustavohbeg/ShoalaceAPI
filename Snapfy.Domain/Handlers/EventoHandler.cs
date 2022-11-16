using Shoalace.Domain.Commands;
using Shoalace.Domain.Commands.Evento;
using Shoalace.Domain.Commands.Usuario;
using Shoalace.Domain.Entities;
using Shoalace.Domain.Enums;
using Shoalace.Domain.Interfaces.Commands;
using Shoalace.Domain.Interfaces.Handlers;
using Shoalace.Domain.Interfaces.Repositories;
using Shoalace.Domain.Interfaces.Services;
using Shoalace.Domain.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shoalace.Domain.Handlers
{
    public class EventoHandler : IHandler<NovoEventoCommand>, IHandler<EditarEventoCommand>
    {
        private readonly IEventoRepository _eventoRepository;
        private readonly IGrupoRepository _grupoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IFileUpload _fileUpload;

        public EventoHandler(IEventoRepository eventoRepository, IGrupoRepository grupoRepository, IUsuarioRepository usuarioRepository, IFileUpload fileUpload)
        {
            _eventoRepository = eventoRepository;
            _grupoRepository = grupoRepository;
            _usuarioRepository = usuarioRepository;
            _fileUpload = fileUpload;
        }

        //NOVO EVENTO
        public async Task<IResultadoCommand> ManipularAsync(NovoEventoCommand comando)
        {
            ResultadoCommand retorno = new();

            comando.Validate();
            if (comando.Invalid)
            {
                retorno.AddNotifications(comando);
                return retorno;
            }

            if (comando.GrupoId > 0)
            {
                if (await _grupoRepository.ObterPorId(comando.GrupoId.Value) == null)
                {
                    retorno.AddNotification("Evento.GrupoId", "Grupo não encontrado");
                    return retorno;
                }
            }

            Evento evento = new(comando.Titulo, comando.Descricao, comando.Local, comando.Valor, comando.Latitude, comando.Longitude, comando.Data, comando.Hora, comando.Tipo, comando.GrupoId != null && comando.GrupoId > 0 ? comando.GrupoId : null, comando.Foto, comando.Categoria);

            List<string> tokens = new();

            if (comando.Membros != null && comando.Membros.Count > 0)
            {
                foreach (MembroEventoCommand membroCommand in comando.Membros)
                {
                    if (!evento.MembroEventoExiste(membroCommand.UsuarioId))
                    {
                        Usuario usuario = await _usuarioRepository.ObterPorId(membroCommand.UsuarioId);
                        if (usuario == null)
                        {
                            retorno.AddNotification("Membro.UsuarioId", "Usuario não encontrado");
                            return retorno;
                        }

                        evento.AdicionarMembroEvento(new(membroCommand.UsuarioId, 0, membroCommand.Comparecer, membroCommand.Admin));

                        if (!string.IsNullOrEmpty(usuario.Token) && !membroCommand.Admin)
                            tokens.Add(usuario.Token);
                    }
                }
            }

            retorno.AddNotifications(evento);

            if (retorno.Valid)
            {
                await _eventoRepository.Adicionar(evento);
                await _eventoRepository.Commit();
                retorno.PreencherRetorno(evento);
                if (tokens.Count > 0)
                    ExpoService.SendNotification(tokens, evento.Titulo, "Você foi convidado para um novo evento");

                if (evento.Categoria != ECategoriaEvento.Privado)
                {
                    List<string> tokensProximos = new();
                    List<Usuario> usuariosProximos = await _usuarioRepository.ObterTodos(); //FILTRAR SOMENTE OS PROXIMOS
                    foreach (Usuario usuarioProximo in usuariosProximos)
                    {
                        if (!evento.MembroEventoExiste(usuarioProximo.Id))
                        {
                            if (!string.IsNullOrEmpty(usuarioProximo.Token))
                                tokens.Add(usuarioProximo.Token);
                        }

                        if (tokensProximos.Count > 0)
                            ExpoService.SendNotification(tokensProximos, evento.Titulo, "Encontramos esse evento perto de você");
                    }
                }
            }

            return retorno;
        }

        //EDITAR EVENTO
        public async Task<IResultadoCommand> ManipularAsync(EditarEventoCommand comando)
        {
            ResultadoCommand retorno = new();

            comando.Validate();
            if (comando.Invalid)
            {
                retorno.AddNotifications(comando);
                return retorno;
            }

            if (comando.GrupoId > 0)
            {
                if (await _grupoRepository.ObterPorId(comando.GrupoId.Value) == null)
                {
                    retorno.AddNotification("Evento.GrupoId", "Grupo não encontrado");
                    return retorno;
                }
            }

            Evento evento = await _eventoRepository.ObterPorId(comando.Id);

            if (evento == null)
            {
                retorno.AddNotification("Evento.Id", "Evento não encontrado");
                return retorno;
            }

            List<string> tokens = new();

            if (comando.Membros != null && comando.Membros.Count > 0)
            {
                foreach (MembroEventoCommand membroCommand in comando.Membros)
                {
                    if (!evento.MembroEventoExiste(membroCommand.UsuarioId))
                    {
                        Usuario usuario = await _usuarioRepository.ObterPorId(membroCommand.UsuarioId);
                        if (usuario == null)
                        {
                            retorno.AddNotification("MembroEvento.UsuarioId", "Usuario não encontrado");
                            return retorno;
                        }

                        evento.AdicionarMembroEvento(new(membroCommand.UsuarioId, 0, membroCommand.Comparecer, membroCommand.Admin));

                        if (!string.IsNullOrEmpty(usuario.Token) && !membroCommand.Admin)
                            tokens.Add(usuario.Token);
                    }
                }
            }

            evento.PreencherEvento(comando.Titulo, comando.Descricao, comando.Local, comando.Valor, comando.Latitude, comando.Longitude, comando.Data, comando.Hora, comando.Tipo, comando.GrupoId != null && comando.GrupoId > 0 ? comando.GrupoId : null, comando.Foto, comando.Categoria);
            retorno.AddNotifications(evento);

            if (retorno.Valid)
            {
                _eventoRepository.Atualizar(evento);
                await _eventoRepository.Commit();
                retorno.PreencherRetorno(evento);
                if (tokens.Count > 0)
                    ExpoService.SendNotification(tokens, evento.Titulo, "Você foi convidado para um novo evento");

                if (evento.Categoria != ECategoriaEvento.Privado)
                {
                    List<string> tokensProximos = new();
                    List<Usuario> usuariosProximos = await _usuarioRepository.ObterTodos(); //FILTRAR SOMENTE OS PROXIMOS
                    foreach (Usuario usuarioProximo in usuariosProximos)
                    {
                        if (!evento.MembroEventoExiste(usuarioProximo.Id))
                        {
                            if (!string.IsNullOrEmpty(usuarioProximo.Token))
                                tokens.Add(usuarioProximo.Token);
                        }

                        if (tokensProximos.Count > 0)
                            ExpoService.SendNotification(tokensProximos, evento.Titulo, "Encontramos esse evento perto de você");
                    }
                }
            }

            return retorno;
        }

        //EXCLUIR EVENTO
        public async Task<IResultadoCommand> ManipularAsync(ExcluirCommand comando)
        {
            ResultadoCommand retorno = new();

            comando.Validate();
            if (comando.Invalid)
            {
                retorno.AddNotifications(comando);
                return retorno;
            }

            Evento evento = await _eventoRepository.ObterPorId(comando.Id);

            if (evento == null)
            {
                retorno.AddNotification("Evento.Id", "Evento não encontrado");
                return retorno;
            }

            if (retorno.Valid)
            {
                _eventoRepository.Remover(evento);
                await _eventoRepository.Commit();
                retorno.PreencherRetorno(evento);
            }

            return retorno;
        }

        //NOVO LISTA EVENTO
        public async Task<IResultadoCommand> ManipularAsync(NovoListaEventoCommand comando)
        {
            ResultadoCommand retorno = new();

            comando.Validate();
            if (comando.Invalid)
            {
                retorno.AddNotifications(comando);
                return retorno;
            }

            Evento evento;
            List<Evento> eventos = new();
            foreach (NovoEventoCommand eventoComando in comando.Eventos)
            {
                evento = new(eventoComando.Titulo, eventoComando.Descricao, eventoComando.Local, eventoComando.Valor, eventoComando.Latitude, eventoComando.Longitude, eventoComando.Data, eventoComando.Hora, eventoComando.Tipo, eventoComando.GrupoId, eventoComando.Foto, eventoComando.Categoria);
                retorno.AddNotifications(evento);
                eventos.Add(evento);
                retorno.AddNotifications(evento);
            }

            if (retorno.Valid)
            {
                await _eventoRepository.AdicionarLista(eventos);
                await _eventoRepository.Commit();
            }

            return retorno;
        }

        //INSERIR MEMBRO
        public async Task<IResultadoCommand> ManipularAsync(NovoMembroEventoCommand comando)
        {
            ResultadoCommand retorno = new();

            comando.Validate();
            if (comando.Invalid)
            {
                retorno.AddNotifications(comando);
                return retorno;
            }

            Evento evento = await _eventoRepository.ObterPorId(comando.EventoId);

            if (evento == null)
            {
                retorno.AddNotification("Evento.Id", "Evento não encontrado");
                return retorno;
            }

            MembroEvento membroEvento = evento.MembrosEvento.FirstOrDefault(a => a.UsuarioId == comando.UsuarioId);
            if (membroEvento?.Id > 0)
            {
                membroEvento.PreencherMembroEvento(comando.UsuarioId, comando.EventoId, comando.Comparecer, comando.Admin);
                evento.FazerCheckIn(membroEvento);
            }
            else
            {
                membroEvento = new(comando.UsuarioId, comando.EventoId, comando.Comparecer, comando.Admin);
                evento.AdicionarMembroEvento(membroEvento);
            }
            if (retorno.Valid)
            {
                _eventoRepository.Atualizar(evento);
                await _eventoRepository.Commit();
                retorno.PreencherRetorno(membroEvento);
            }

            return retorno;
        }

        //EDITAR MEMBRO
        public async Task<IResultadoCommand> ManipularAsync(EditarMembroEventoCommand comando)
        {
            ResultadoCommand retorno = new();

            comando.Validate();
            if (comando.Invalid)
            {
                retorno.AddNotifications(comando);
                return retorno;
            }

            Evento evento = await _eventoRepository.ObterPorId(comando.EventoId);

            if (evento == null)
            {
                retorno.AddNotification("Evento.Id", "Evento não encontrado");
                return retorno;
            }

            MembroEvento membroEvento = evento.MembrosEvento.FirstOrDefault(a => a.Id == comando.Id || a.UsuarioId == comando.UsuarioId);
            membroEvento.PreencherMembroEvento(comando.UsuarioId, comando.EventoId, comando.Comparecer, comando.Admin);

            if (retorno.Valid)
            {
                evento.FazerCheckIn(membroEvento);
                _eventoRepository.Atualizar(evento);
                await _eventoRepository.Commit();
                retorno.PreencherRetorno(membroEvento);
            }
            return retorno;
        }

        public IResultadoCommand Manipular(UploadMediaCommand comando)
        {
            ResultadoCommand retorno = new();
            retorno.PreencherRetorno(_fileUpload.UploadBase64Image(comando.Base64, "blobs", comando.Formato));
            return retorno;
        }
    }
}
