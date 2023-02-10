using Microsoft.EntityFrameworkCore;
using PolicyPermission.Abstraction.Data;
using PolicyPermission.Data.Repository.Base;
using PolicyPermission.Entity.Entities;

namespace PolicyPermission.Data.Repository
{
    internal class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext db) : base(db)
        {
        }

        public async Task<User?> GetByGuid(Guid guid) => await Queryable.FirstOrDefaultAsync(x => x.Guid == guid);
    }
}