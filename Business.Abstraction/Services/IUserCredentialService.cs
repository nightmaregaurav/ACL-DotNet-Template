using Business.Contracts.RequestModels;
using Business.Contracts.ResponseModels;

namespace Business.Abstraction.Services
{
    public interface IUserCredentialService
    {
        Task<Guid> Add(UserCredentialAddRequestModel model);
        Task Update(UserCredentialUpdateRequestModel model);
        Task Delete(Guid guid);
        Task<IEnumerable<UserCredentialResponseModel>> GetAll();
    }
}