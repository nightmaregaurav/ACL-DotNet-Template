using ACL.Entity.Entities;

namespace ACL.Abstraction.Data
{
    public interface IUserCredentialRepository
    {
        new Task<UserCredential?> GetById(int id);
        Task Insert(UserCredential credential);
        Task Update(UserCredential credential);
        Task Delete(UserCredential credential);
        Task<IEnumerable<UserCredential>> GetAll();
        Task<UserCredential?> GetByGuid(Guid guid);
        Task<UserCredential?> GetByUsername(string username);
        Task<UserCredential?> GetByUser(Guid userGuid);
    }
}