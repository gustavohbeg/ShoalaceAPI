using Shoalace.Domain.Entities;
using Shoalace.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shoalace.Domain.Interfaces.Repositories
{
    public interface IEventoRepository : IBaseRepository<Evento>
    {
        new Task<Evento> ObterPorId(long id);
        Task<List<Evento>> ObterTodosPorUsuario(long UsuarioId);
        Task<List<Evento>> ObterTodosExplorar();
        Task<List<Evento>> ObterProximosPorUsuario(long UsuarioId);
        Task<List<Evento>> ObterTodosPorData(DateTime data);
        Task<List<Evento>> ObterPorCategoriaECidade(ECategoria categoria, string cidade);
    }
}
