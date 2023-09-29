using Data.Models.Entities;

namespace Data.Abstraction.Repositories
{
    public interface IRoleRepository
    {
        Task Update(Role role);
        Task<Role?> GetByGuid(string guid);
    }
}
