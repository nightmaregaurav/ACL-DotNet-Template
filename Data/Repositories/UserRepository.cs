using Data.Abstraction.Repositories;
using Data.Models.Entities;
using Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    internal class UserRepository(AppDbContext db) : BaseRepository<User>(db), IUserRepository
    {
        public async Task<User?> GetByGuidAsync(string guid) => await Queryable.FirstOrDefaultAsync(x => x.Guid == guid).ConfigureAwait(false);
    }
}
