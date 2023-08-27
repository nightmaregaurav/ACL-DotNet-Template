using Business.Contracts.RequestModels;
using Business.Contracts.ResponseModels;

namespace Business.Abstraction
{
    public interface IUserService
    {
        Task<Guid> Add(UserAddRequestModel model);
        Task Update(UserUpdateRequestModel model);
        Task Delete(Guid guid);
        Task<IEnumerable<UserResponseModel>> GetAll();
        Task<IEnumerable<string>> SetAndGetNewPermissions(UserPermissionSetRequestModel model);
        Task<IEnumerable<string>> GetPermissions(Guid guid);
        Task<IEnumerable<string>> GetPermissions();
    }
}