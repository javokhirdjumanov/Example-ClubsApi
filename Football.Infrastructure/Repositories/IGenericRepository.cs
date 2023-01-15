using System.Linq.Expressions;

namespace Football.Infrastructure.Repositories
{
    public interface IGenericRepository<TEntity, TKey>
    {
        ValueTask<TEntity> InsertAsync(TEntity entity);
        IQueryable<TEntity> SelectAll();
        ValueTask<TEntity> SelectByIdAsync(TKey id);
        ValueTask<TEntity> SelectByIdWithDetaialsAsync(Expression<Func<TEntity, bool>> expression, string[] includes);
        ValueTask<TEntity> UpdateAsync(TEntity entity);
        ValueTask<TEntity> DeleteAsync(TEntity entity);
        ValueTask<int> SaveChangesAsync();
    }
}
