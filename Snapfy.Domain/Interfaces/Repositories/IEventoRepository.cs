using Shoalace.Domain.Entities;
using Shoalace.Domain.Enums;
using Shoalace.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shoalace.Domain.Interfaces.Repositories
{
    public interface IEventoRepository : IBaseRepository<Evento>
    {
        Task<List<Evento>> ObterTodosPorUsuario(long UsuarioId);
        Task<List<Evento>> ObterPor2Usuarios(long usuarioId, long contatoId);
        Task<List<EventoResponse>> ObterResponsesPor2Usuarios(long usuarioId, long contatoId);
        Task<List<Evento>> ObterTodosExplorar();
        Task<List<Evento>> ObterProximosPorUsuario(long UsuarioId);
        Task<List<Evento>> ObterTodosPorData(DateTime data);
        Task<List<Evento>> ObterPorCategoriaECidade(ECategoriaEvento categoria, string cidade);
    }
}
