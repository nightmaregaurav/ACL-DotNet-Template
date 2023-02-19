using ACL.Abstraction.Data.Base;
using ACL.Entity.Entities;

namespace ACL.Abstraction.Data
{
    public interface IUserRepository : IBaseRepository<User>
    {
        new Task<User?> GetById(int id);
        Task<User?> GetByGuid(Guid guid);
    }
}