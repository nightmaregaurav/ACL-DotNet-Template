using Data.Abstraction.Repositories;
using Data.Entity.Entities;
using Data.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    internal class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext db) : base(db)
        {
        }

        public async Task<User?> GetByGuid(Guid guid) => await Queryable.FirstOrDefaultAsync(x => x.Guid == guid);
    }
}