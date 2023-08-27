using Business.Contracts.RequestModels;
using Business.Contracts.ResponseModels;

namespace Business.Abstraction.Services
{
    public interface IUserService
    {
        Task<Guid> Add(UserAddRequestModel model);
        Task Update(UserUpdateRequestModel model);
        Task Delete(Guid guid);
        Task<IEnumerable<UserResponseModel>> GetAll();
        Task<IEnumerable<string>> SetAndGetNewPermissions(UserPermissionSetRequestModel model);
        Task<IEnumerable<string>> GetPermissions(Guid guid);
    }
}
