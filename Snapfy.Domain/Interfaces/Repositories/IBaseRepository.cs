using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shoalace.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<TEntity> : IDisposable where TEntity : class
    {
        Task Adicionar(TEntity entity);
        Task AdicionarLista(IEnumerable<TEntity> entities);
        Task<TEntity> ObterPorId(long id);
        Task<List<TEntity>> ObterTodos();
        void Atualizar(TEntity entity);
        void AtualizarLista(IEnumerable<TEntity> entities);
        void Remover(TEntity entity);
        void RemoverLista(IEnumerable<TEntity> entities);
        Task Commit();
    }
}
