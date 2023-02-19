using ACL.Abstraction.Data;
using ACL.Data.Repository.Base;
using ACL.Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace ACL.Data.Repository
{
    internal class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext db) : base(db)
        {
        }

        public async Task<User?> GetByGuid(Guid guid) => await Queryable.FirstOrDefaultAsync(x => x.Guid == guid);
    }
}