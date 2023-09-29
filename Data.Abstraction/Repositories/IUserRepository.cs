using Data.Models.Entities;

namespace Data.Abstraction.Repositories
{
    public interface IUserRepository
    {
        Task Update(User user);
        Task<User?> GetByGuid(string guid);
    }
}
