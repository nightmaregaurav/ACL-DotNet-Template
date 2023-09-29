using Data.Models.Entities;

namespace Data.Abstraction.Repositories
{
    public interface IRoleRepository
    {
        Task UpdateAsync(Role role);
        Task<Role?> GetByGuidAsync(string guid);
    }
}
