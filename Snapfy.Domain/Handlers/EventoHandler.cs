using Shoalace.Domain.Commands;
using Shoalace.Domain.Commands.Evento;
using Shoalace.Domain.Commands.Usuario;
using Shoalace.Domain.Entities;
using Shoalace.Domain.Interfaces.Commands;
using Shoalace.Domain.Interfaces.Handlers;
using Shoalace.Domain.Interfaces.Repositories;
using Shoalace.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shoalace.Domain.Handlers
{
    public class EventoHandler : IHandler<NovoEventoCommand>, IHandler<EditarEventoCommand>
    {
        private readonly IEventoRepository _eventoRepository;
        private readonly IFileUpload _fileUpload;

        public EventoHandler(IEventoRepository eventoRepository, IFileUpload fileUpload)
        {
            _eventoRepository = eventoRepository;
            _fileUpload = fileUpload;
        }
        public async Task<IResultadoCommand> ManipularAsync(NovoEventoCommand comando)
        {
            ResultadoCommand retorno = new();

            comando.Validate();
            if (comando.Invalid)
            {
                retorno.AddNotifications(comando);
                return retorno;
            }

            Evento evento = new(comando.Titulo, comando.Descricao, comando.Local, comando.Valor, comando.Latitude, comando.Longitude, comando.Data, comando.Hora, comando.Tipo, comando.GrupoId, comando.Foto, comando.Categoria);

            foreach (MembroEventoCommand membroCommand in comando.Membros)
            {
                evento.AdicionarMembroEvento(new MembroEvento(membroCommand.UsuarioId, 0, membroCommand.Comparecer, membroCommand.Admin));
            }

            if (retorno.Valid)
            {
                await _eventoRepository.Adicionar(evento);
                await _eventoRepository.Commit();
                retorno.PreencherRetorno(evento);
            }

            return retorno;
        }

        public async Task<IResultadoCommand> ManipularAsync(EditarEventoCommand comando)
        {
            ResultadoCommand retorno = new();

            comando.Validate();
            if (comando.Invalid)
            {
                retorno.AddNotifications(comando);
                return retorno;
            }

            Evento evento = await _eventoRepository.ObterPorId(comando.Id);
            foreach (MembroEventoCommand membroCommand in comando.Membros)
            {
                evento.AdicionarMembroEvento(new MembroEvento(membroCommand.UsuarioId, 0, membroCommand.Comparecer, membroCommand.Admin));
            }

            if (evento == null)
            {
                retorno.AddNotification("Evento.Id", "Evento não encontrado");
                return retorno;
            }

            evento.PreencherEvento(comando.Titulo, comando.Descricao, comando.Local, comando.Valor, comando.Latitude, comando.Longitude, comando.Data, comando.Hora, comando.Tipo, comando.GrupoId, comando.Foto, comando.Categoria);

            if (retorno.Valid)
            {
                _eventoRepository.Atualizar(evento);
                await _eventoRepository.Commit();
                retorno.PreencherRetorno(evento);
            }

            return retorno;
        }

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
            }

            if (retorno.Valid)
            {
                await _eventoRepository.AdicionarLista(eventos);
                await _eventoRepository.Commit();
            }

            return retorno;
        }

        public async Task<IResultadoCommand> ManipularAsync(InserirMembroEventoCommand comando)
        {
            ResultadoCommand retorno = new();

            comando.Validate();
            if (comando.Invalid)
            {
                retorno.AddNotifications(comando);
                return retorno;
            }
            Evento evento = await _eventoRepository.ObterPorId(comando.EventoId);
            MembroEvento membroEvento; 
            membroEvento = evento.MembrosEvento.FirstOrDefault(a => a.UsuarioId == comando.UsuarioId);
            if (membroEvento != null && membroEvento.Id > 0)
            {
                membroEvento.PreencherMembroEvento(comando.UsuarioId, comando.EventoId, comando.Comparecer, comando.Admin);
                retorno.AddNotifications(membroEvento);
                if (retorno.Valid)
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

            MembroEvento membroEvento = evento.MembrosEvento.FirstOrDefault(a => a.Id == comando.Id || a.UsuarioId == comando.UsuarioId);
            membroEvento.PreencherMembroEvento(comando.UsuarioId, comando.EventoId, comando.Comparecer, comando.Admin);
            retorno.AddNotifications(membroEvento);
            if (retorno.Valid)
                evento.FazerCheckIn(membroEvento);

            if (retorno.Valid)
            {
                _eventoRepository.Atualizar(evento);
                await _eventoRepository.Commit();
                retorno.PreencherRetorno(membroEvento);
            }

            return retorno;
        }

        public async Task<IResultadoCommand> ManipularAsync(UploadImageCommand comando)
        {
            ResultadoCommand retorno = new();
            retorno.PreencherRetorno(_fileUpload.UploadBase64Image(comando.Base64, "blobs"));
            return retorno;
        }
    }
}
