using ACL.Entity.Entities;

namespace ACL.Abstraction.Data
{
    public interface IUserRepository
    {
        Task Insert(User user);
        Task Update(User user);
        Task Delete(User user);
        Task<IEnumerable<User>> GetAll();
        new Task<User?> GetById(int id);
        Task<User?> GetByGuid(Guid guid);
    }
}