using PolicyPermission.Abstraction.Data.Base;
using PolicyPermission.Entity.Entities;

namespace PolicyPermission.Abstraction.Data
{
    public interface IUserRepository : IBaseRepository<User>
    {
        new Task<User?> GetById(int id);
        Task<User?> GetByGuid(Guid guid);
    }
}