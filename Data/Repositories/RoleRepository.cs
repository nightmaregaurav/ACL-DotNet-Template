using Data.Abstraction.Repositories;
using Data.Models.Entities;
using Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    internal class RoleRepository(AppDbContext db) : BaseRepository<Role>(db), IRoleRepository
    {
        public async Task<Role?> GetByGuidAsync(string guid) => await Queryable.FirstOrDefaultAsync(x => x.Guid == guid).ConfigureAwait(false);
    }
}
