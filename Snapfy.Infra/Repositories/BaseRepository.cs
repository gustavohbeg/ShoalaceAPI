using Microsoft.EntityFrameworkCore;
using Shoalace.Domain.Interfaces.Repositories;
using Shoalace.Infra.Contexto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shoalace.Infra.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        //Nesse caso usaremos protected pois queremos que as filhas dessa classe acessem essa propriedade, ao invés de apenas essa classe acessa-la(private)
        protected readonly ShoalaceContexto _ShoalaceContexto;

        public BaseRepository(ShoalaceContexto ShoalaceContexto)
        {
            _ShoalaceContexto = ShoalaceContexto;
        }

        public async Task Adicionar(TEntity entity)
        {
            await _ShoalaceContexto.Set<TEntity>().AddAsync(entity);
        }

        public async Task AdicionarLista(IEnumerable<TEntity> entities)
        {
            await _ShoalaceContexto.Set<TEntity>().AddRangeAsync(entities);
        }

        public void Atualizar(TEntity entity)
        {
            _ShoalaceContexto.Set<TEntity>().Update(entity);
        }

        public void AtualizarLista(IEnumerable<TEntity> entities)
        {
            _ShoalaceContexto.Set<TEntity>().UpdateRange(entities);
        }

        public async Task<TEntity> ObterPorId(int id)
        {
            return await _ShoalaceContexto.Set<TEntity>().FindAsync(id);
        }

        public void Remover(TEntity entity)
        {
            _ShoalaceContexto.Remove(entity);
        }

        public void RemoverLista(IEnumerable<TEntity> entities)
        {
            _ShoalaceContexto.RemoveRange(entities);
        }

        public async Task Commit()
        {
            await _ShoalaceContexto.SaveChangesAsync();
        }

        public void Dispose()
        {
            _ShoalaceContexto.Dispose();
        }

        public async Task<List<TEntity>> ObterTodos()
        {
            return await _ShoalaceContexto.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> ObterPorId(long id)
        {
            return await _ShoalaceContexto.Set<TEntity>().FindAsync(id);
        }
    }
}
