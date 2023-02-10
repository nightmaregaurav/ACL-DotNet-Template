using Microsoft.EntityFrameworkCore;
using PolicyPermission.Abstraction.Data.Base;

namespace PolicyPermission.Data.Repository.Base
{
    internal class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        public IQueryable<T> Queryable { get; }
        
        private readonly AppDbContext _db;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(AppDbContext db)
        {
            _db = db;
            _dbSet = db.Set<T>();
            Queryable = _dbSet.AsQueryable();
        }

        public async Task Add(T entity)
        {
            await _dbSet.AddAsync(entity);
            await Save();
        }

        public async Task AddRange(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            await Save();
        }

        public async Task Update(T entity)
        {
            _dbSet.Update(entity);
            await Save();
        }

        public async Task UpdateRange(IEnumerable<T> entities)
        {
            _dbSet.UpdateRange(entities);
            await Save();
        }

        public async Task Remove(T entity)
        {
            _dbSet.Remove(entity);
            await Save();
        }

        public async Task RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
            await Save();
        }

        public async Task<T?> GetById(int id) => await _dbSet.FindAsync(id);
        public async Task<IEnumerable<T>> GetAll() => await _dbSet.ToListAsync();
        public async Task<int> Count() => await _dbSet.CountAsync();

        private async Task Save() => await _db.SaveChangesAsync();
    }
}