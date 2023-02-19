namespace ACL.Abstraction.Data.Base
{
    public interface IBaseRepository<T> where T : class
    {
        public IQueryable<T> Queryable { get; }
        
        Task Add(T entity);
        Task AddRange(IEnumerable<T> entities);
        
        Task Update(T entity);
        Task UpdateRange(IEnumerable<T> entities);
        
        Task Remove(T entity);
        Task RemoveRange(IEnumerable<T> entities);
        
        Task<T?> GetById(int id);
        Task<IEnumerable<T>> GetAll();
        Task<int> Count();
    }
}