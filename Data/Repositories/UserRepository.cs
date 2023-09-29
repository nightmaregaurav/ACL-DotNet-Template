using Data.Abstraction.Repositories;
using Data.Models.Entities;
using Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    internal class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext db) : base(db)
        {
        }

        public async Task<User?> GetByGuid(string guid) => await Queryable.FirstOrDefaultAsync(x => x.Guid == guid);
    }
}
