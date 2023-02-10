using Microsoft.EntityFrameworkCore;
using PolicyPermission.Abstraction.Data;
using PolicyPermission.Data.Repository.Base;
using PolicyPermission.Entity.Entities;

namespace PolicyPermission.Data.Repository
{
    internal class UserCredentialRepository : BaseRepository<UserCredential>, IUserCredentialRepository
    {
        public UserCredentialRepository(AppDbContext db) : base(db)
        {
        }

        public async Task<UserCredential?> GetByGuid(Guid guid) => await Queryable.FirstOrDefaultAsync(x => x.Guid == guid);
        public async Task<UserCredential?> GetCredentialByUsername(string username) => await Queryable.FirstOrDefaultAsync(x => x.UserName == username);
        public async Task<UserCredential?> GetCredentialByUser(Guid userGuid) => await Queryable.FirstOrDefaultAsync(x => x.User.Guid == userGuid);
    }
}