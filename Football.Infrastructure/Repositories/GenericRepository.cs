using Football.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Football.Infrastructure.Repositories
{
    public abstract class GenericRepository<TEntity, TKey>
        : IGenericRepository<TEntity, TKey> where TEntity : class
    {
        /// <summary>
        /// C O N S T R U C T O R
        /// </summary>
        private readonly AppDbContext context;
        public GenericRepository(AppDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// I N S E R T   User from Database
        /// </summary>
        public async ValueTask<TEntity> InsertAsync(TEntity entity)
        {
            var entityEntry = await this.context
                .AddAsync<TEntity>(entity);

            await this.SaveChangesAsync();

            return entityEntry.Entity;
        }

        /// <summary> 
        /// G E T   Users and User that by Id and with User Details
        /// </summary>
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
        /// U P D A T E   user from Database
        /// </summary>
        public async ValueTask<TEntity> UpdateAsync(TEntity entity)
        {
            var entityEntry = this.context
                .Update<TEntity>(entity);

            await this.SaveChangesAsync();

            return entityEntry.Entity;
        }

        /// <summary>
        /// D E L E T E   User from database
        /// </summary>
        public async ValueTask<TEntity> DeleteAsync(TEntity entity)
        {
            var entityEntry = this.context
                .Set<TEntity>()
                .Remove(entity);

            await this.SaveChangesAsync();

            return entityEntry.Entity;
        }

        /// <summary>
        /// S A V E   Changes
        /// </summary>
        public async ValueTask<int> SaveChangesAsync() =>
            await this.context.SaveChangesAsync();
    }
}
