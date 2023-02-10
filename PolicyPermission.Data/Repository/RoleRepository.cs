using Microsoft.EntityFrameworkCore;
using PolicyPermission.Abstraction.Data;
using PolicyPermission.Data.Repository.Base;
using PolicyPermission.Entity.Entities;

namespace PolicyPermission.Data.Repository
{
    internal class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(AppDbContext db) : base(db)
        {
        }

        public async Task<Role?> GetByGuid(Guid guid) => await Queryable.FirstOrDefaultAsync(x => x.Guid == guid);
        public async Task<Role?> GetRoleByName(string roleName) => await Queryable.FirstOrDefaultAsync(x => x.Name == roleName);
    }
}