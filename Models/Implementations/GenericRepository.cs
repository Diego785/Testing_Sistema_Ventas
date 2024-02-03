using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TestingDBVenta.Models.Interfaces;

namespace TestingDBVenta.Models.Implementations
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly DbventaContext _dbventaContext;

        public GenericRepository(DbventaContext dbventaContext)
        {
            _dbventaContext = dbventaContext;
        }

        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> filter)
        {
            try
            {
                TEntity entity = await _dbventaContext.Set<TEntity>().FirstOrDefaultAsync(filter);
                return entity;
            }
            catch
            {
                throw;
            }
        }
 

        public async Task<TEntity> Create(TEntity entity)
        {
            try
            {
                _dbventaContext.Set<TEntity>().Add(entity);
                await _dbventaContext.SaveChangesAsync();
                return entity;
            }
            catch
            {
                throw;
            }

        }

        public async Task<bool> Edit(TEntity entity)
        {
            try
            {
                _dbventaContext.Update(entity);
                await _dbventaContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Delete(TEntity entity)
        {
            try
            {
                _dbventaContext.Remove(entity);
                await _dbventaContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IQueryable<TEntity>> Consult(Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity> queryEntity = filter == null ? _dbventaContext.Set<TEntity>() : _dbventaContext.Set<TEntity>().Where(filter);
            return queryEntity;
        }

    }
}
