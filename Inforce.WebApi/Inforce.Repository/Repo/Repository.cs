using Inforce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Inforce.Repository.Repo
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly InforceContext _context;
        private DbSet<TEntity> _entities;

        public Repository(InforceContext context)
        {
            _context = context;
            _entities = _context.Set<TEntity>();
        }
        public async Task AddAsync(TEntity entity)
        {
            await _entities.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _entities.ToListAsync();
        }

        public async Task<TEntity?> GetAsync(int id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task<bool> RemoveAsync(int id)
        {
            var entity = await _entities.FindAsync(id);
            if (entity != null)
            {
                _entities.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            else { return false; }
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            if(!_entities.Any(x => x.Id == entity.Id))
            {
                return false;
            }
            _entities.Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _entities.Where(expression).SingleOrDefaultAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
