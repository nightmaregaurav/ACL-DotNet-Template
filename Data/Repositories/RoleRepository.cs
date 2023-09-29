using Data.Abstraction.Repositories;
using Data.Models.Entities;
using Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    internal class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(AppDbContext db) : base(db)
        {
        }

        public async Task<Role?> GetByGuid(string guid) => await Queryable.FirstOrDefaultAsync(x => x.Guid == guid);
    }
}
