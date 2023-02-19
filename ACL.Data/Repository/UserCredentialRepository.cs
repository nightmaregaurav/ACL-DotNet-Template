using ACL.Abstraction.Data;
using ACL.Data.Repository.Base;
using ACL.Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace ACL.Data.Repository
{
    internal class UserCredentialRepository : BaseRepository<UserCredential>, IUserCredentialRepository
    {
        public UserCredentialRepository(AppDbContext db) : base(db)
        {
        }

        public async Task<UserCredential?> GetByGuid(Guid guid) => await Queryable.FirstOrDefaultAsync(x => x.Guid == guid);
        public async Task<UserCredential?> GetByUsername(string username) => await Queryable.FirstOrDefaultAsync(x => x.UserName == username);
        public async Task<UserCredential?> GetByUser(Guid userGuid) => await Queryable.FirstOrDefaultAsync(x => x.User.Guid == userGuid);
    }
}