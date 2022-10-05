
using System.Linq.Expressions;

namespace Inforce.Repository.Repo
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetAsync(int id);
        Task AddAsync(TEntity entity);
        Task<bool> RemoveAsync(int id);
        Task<bool> UpdateAsync(TEntity entity);
        Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> expression);
        Task SaveChangesAsync();
    }
}
