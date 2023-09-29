using Data.Models.Entities;

namespace Data.Abstraction.Repositories
{
    public interface IUserRepository
    {
        Task UpdateAsync(User user);
        Task<User?> GetByGuidAsync(string guid);
    }
}
