using ACL.Abstraction.Data.Base;
using ACL.Entity.Entities;

namespace ACL.Abstraction.Data
{
    public interface IUserCredentialRepository : IBaseRepository<UserCredential>
    {
        new Task<UserCredential?> GetById(int id);
        Task<UserCredential?> GetByGuid(Guid guid);
        Task<UserCredential?> GetByUsername(string username);
        Task<UserCredential?> GetByUser(Guid userGuid);
    }
}