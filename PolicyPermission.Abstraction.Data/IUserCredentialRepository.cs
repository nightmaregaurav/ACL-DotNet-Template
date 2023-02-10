using PolicyPermission.Abstraction.Data.Base;
using PolicyPermission.Entity.Entities;

namespace PolicyPermission.Abstraction.Data
{
    public interface IUserCredentialRepository : IBaseRepository<UserCredential>
    {
        new Task<UserCredential?> GetById(int id);
        Task<UserCredential?> GetByGuid(Guid guid);
        Task<UserCredential?> GetCredentialByUsername(string username);
        Task<UserCredential?> GetCredentialByUser(Guid userGuid);
    }
}