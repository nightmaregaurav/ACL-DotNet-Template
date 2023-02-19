using ACL.Abstraction.Data.Base;
using ACL.Entity.Entities;

namespace ACL.Abstraction.Data
{
    public interface IRoleRepository : IBaseRepository<Role>
    {
        new Task<Role?> GetById(int id);
        Task<Role?> GetByGuid(Guid guid);
        Task<Role?> GetByName(string roleName);
    }
}