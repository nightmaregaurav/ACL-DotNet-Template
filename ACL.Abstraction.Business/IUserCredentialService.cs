using ACL.Contracts.RequestModels;
using ACL.Contracts.ResponseModels;

namespace ACL.Abstraction.Business
{
    public interface IUserCredentialService
    {
        Task<Guid> Add(UserCredentialAddRequestModel model);
        Task Update(UserCredentialUpdateRequestModel model);
        Task Delete(Guid guid);
        Task<IEnumerable<UserCredentialResponseModel>> GetAll();
    }
}