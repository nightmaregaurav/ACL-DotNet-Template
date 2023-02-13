using PolicyPermission.Contracts.RequestModels;
using PolicyPermission.Contracts.ResponseModels;

namespace PolicyPermission.Abstraction.Business
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