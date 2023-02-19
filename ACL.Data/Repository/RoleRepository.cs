using ACL.Abstraction.Data;
using ACL.Data.Repository.Base;
using ACL.Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace ACL.Data.Repository
{
    internal class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(AppDbContext db) : base(db)
        {
        }

        public async Task<Role?> GetByGuid(Guid guid) => await Queryable.FirstOrDefaultAsync(x => x.Guid == guid);
        public async Task<Role?> GetByName(string roleName) => await Queryable.FirstOrDefaultAsync(x => x.Name == roleName);
    }
}