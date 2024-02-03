using System.Linq.Expressions;

namespace TestingDBVenta.Models.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<TEntity> Get(Expression<Func<TEntity, bool>> filter);
        Task<IQueryable<TEntity>> Consult(Expression<Func<TEntity, bool>> filter);

        Task<TEntity> Create(TEntity entity);
        Task<bool> Edit(TEntity entity);
        Task<bool> Delete(TEntity entity);

    }
}
