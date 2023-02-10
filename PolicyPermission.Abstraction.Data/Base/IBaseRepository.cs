namespace PolicyPermission.Abstraction.Data.Base
{
    public interface IBaseRepository<T> where T : class
    {
        public IQueryable<T> Queryable { get; }
        
        Task Add(T entity);
        Task AddRange(IEnumerable<T> entities);
        
        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);
        
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        
        Task<T?> GetById(int id);
        Task<IEnumerable<T>> GetAll();
    }
}