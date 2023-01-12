using Football.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Football.Infrastructure.Repositories
{
    public abstract class GenericRepository<TEntity, TKey>
        : IGenericRepository<TEntity, TKey> where TEntity : class
    {
        /// <summary>
        /// contructor
        /// </summary>
        private readonly AppDbContext context;
        public GenericRepository(AppDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Insert user from Database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async ValueTask<TEntity> InsertAsync(TEntity entity)
        {
            var entityEntry = await this.context
                .AddAsync<TEntity>(entity);

            await this.SaveChangesAsync();

            return entityEntry.Entity;
        }

        /// <summary> 
        /// get users and user that by Id and with User Details
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> SelectAll()
        {
            return this.context.Set<TEntity>();
        }
        public ValueTask<TEntity> SelectByIdAsync(TKey id)
        {
            return this.context.Set<TEntity>().FindAsync(id);
        }
        public async ValueTask<TEntity> SelectByIdWithDetaialsAsync(
            Expression<Func<TEntity, bool>> expression, string[] includes = null)
        {
            IQueryable<TEntity> entities = this.SelectAll();

            foreach (var inc in includes)
            {
                entities = entities.Include(inc);
            }

            return await entities.FirstOrDefaultAsync(expression);
        }

        /// <summary>
        /// Update user from Database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async ValueTask<TEntity> UpdateAsync(TEntity entity)
        {
            var entityEntry = this.context
                .Update<TEntity>(entity);

            await this.SaveChangesAsync();

            return entityEntry.Entity;
        }

        /// <summary>
        /// Delete user from database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async ValueTask<TEntity> DeleteAsync(TEntity entity)
        {
            var entityEntry = this.context
                .Set<TEntity>()
                .Remove(entity);

            await this.SaveChangesAsync();

            return entityEntry.Entity;
        }

        /// <summary>
        /// save changes
        /// </summary>
        /// <returns></returns>
        public async ValueTask<int> SaveChangesAsync() =>
            await this.context.SaveChangesAsync();
    }
}
