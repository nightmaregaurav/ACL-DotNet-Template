using PolicyPermission.Contracts.RequestModels;
using PolicyPermission.Contracts.ResponseModels;

namespace PolicyPermission.Abstraction.Business
{
    public interface IUserCredentialService
    {
        Task<Guid> AddCredential(UserCredentialAddRequestModel model);
        Task UpdateCredential(UserCredentialUpdateRequestModel model);
        Task DeleteCredential(Guid guid);
        Task<IEnumerable<UserCredentialResponseModel>> GetAllCredentials();
        Task<UserCredentialResponseModel> GetCredentialByUserName(string username);
    }
}