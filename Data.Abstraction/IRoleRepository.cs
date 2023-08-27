using Data.Entity.Entities;

namespace Data.Abstraction
{
    public interface IRoleRepository
    {
        new Task<Role?> GetById(int id);
        Task Insert(Role role);
        Task Update(Role role);
        Task Delete(Role role);
        Task<IEnumerable<Role>> GetAll();
        Task<Role?> GetByGuid(Guid guid);
        Task<Role?> GetByName(string roleName);
    }
}