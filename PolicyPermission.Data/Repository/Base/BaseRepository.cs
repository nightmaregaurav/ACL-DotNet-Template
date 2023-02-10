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

        public async Task Add(T entity) => await _dbSet.AddAsync(entity);
        public async Task AddRange(IEnumerable<T> entities) => await _dbSet.AddRangeAsync(entities);
        
        public void Update(T entity)
        {
            _dbSet.Update(entity);
            Save();
        }

        public void UpdateRange(IEnumerable<T> entities) => _dbSet.UpdateRange(entities);
        
        public void Remove(T entity) => _dbSet.Remove(entity);
        public void RemoveRange(IEnumerable<T> entities) => _dbSet.RemoveRange(entities);
        
        public async Task<T?> GetById(int id) => await _dbSet.FindAsync(id);
        public async Task<IEnumerable<T>> GetAll() => await _dbSet.ToListAsync();

        public void Save() => _db.SaveChangesAsync();
    }
}