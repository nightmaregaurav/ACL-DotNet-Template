using PolicyPermission.Contracts.RequestModels;
using PolicyPermission.Contracts.ResponseModels;

namespace PolicyPermission.Abstraction.Business
{
    public interface IUserCredentialService
    {
        Task<Guid> Add(UserCredentialAddRequestModel model);
        Task Update(UserCredentialUpdateRequestModel model);
        Task Delete(Guid guid);
        Task<IEnumerable<UserCredentialResponseModel>> GetAll();
    }
}