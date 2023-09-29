using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Base
{
    internal class BaseRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _table;

        public IQueryable<T> Queryable { get; }

        public BaseRepository(AppDbContext context)
        {
            _context = context;
            _table = context.Set<T>();
            Queryable = _table.AsQueryable();
        }

        public void SaveChanges() => _context.SaveChanges();
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

        public void Insert(T entity)
        {
            _table.Add(entity);
            SaveChanges();
        }

        public async Task InsertAsync(T entity)
        {
            await _table.AddAsync(entity);
            await SaveChangesAsync();
        }

        public void InsertRange(IEnumerable<T> entities)
        {
            _table.AddRange(entities);
            SaveChanges();
        }

        public async Task InsertRangeAsync(IEnumerable<T> entities)
        {
            await _table.AddRangeAsync(entities);
            await SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _table.Update(entity);
            SaveChanges();
        }

        public async Task UpdateAsync(T entity)
        {
            _table.Update(entity);
            await SaveChangesAsync();
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            _table.UpdateRange(entities);
            SaveChanges();
        }

        public async Task UpdateRangeAsync(IEnumerable<T> entities)
        {
            _table.UpdateRange(entities);
            await SaveChangesAsync();
        }

        public void Delete(T entity)
        {
            _table.Remove(entity);
            SaveChanges();
        }

        public async Task DeleteAsync(T entity)
        {
            _table.Remove(entity);
            await SaveChangesAsync();
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _table.RemoveRange(entities);
            SaveChanges();
        }

        public async Task DeleteRangeAsync(IEnumerable<T> entities)
        {
            _table.RemoveRange(entities);
            await SaveChangesAsync();
        }

        public long Count() => Queryable.Count();
        public async Task<long> CountAsync() => await Queryable.CountAsync();

        public T? GetById(long id) => _table.Find(id);
        public async Task<T?> GetByIdAsync(long id) => await _table.FindAsync(id);

        public ICollection<T> GetAll() => Queryable.ToList();
        public async Task<ICollection<T>> GetAllAsync() => await Queryable.ToListAsync();
    }
}
