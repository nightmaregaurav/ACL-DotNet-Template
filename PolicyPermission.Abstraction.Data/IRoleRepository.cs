using PolicyPermission.Abstraction.Data.Base;
using PolicyPermission.Entity.Entities;

namespace PolicyPermission.Abstraction.Data
{
    public interface IRoleRepository : IBaseRepository<Role>
    {
        new Task<Role?> GetById(int id);
        Task<Role?> GetByGuid(Guid guid);
        Task<Role?> GetRoleByName(string roleName);
    }
}