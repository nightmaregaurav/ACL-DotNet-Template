using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Base
{
    internal class BaseRepository<T> where T : class
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

        public async Task Insert(T entity)
        {
            await _dbSet.AddAsync(entity);
            await Save();
        }

        public async Task InsertRange(IEnumerable<T> entities)
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

        public async Task Delete(T entity)
        {
            _dbSet.Remove(entity);
            await Save();
        }

        public async Task DeleteRange(IEnumerable<T> entities)
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
