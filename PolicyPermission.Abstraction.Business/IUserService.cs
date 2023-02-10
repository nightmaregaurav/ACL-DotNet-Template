using PolicyPermission.Contracts.RequestModels;
using PolicyPermission.Contracts.ResponseModels;

namespace PolicyPermission.Abstraction.Business
{
    public interface IUserService
    {
        Task<Guid> AddUser(UserAddRequestModel model);
        Task UpdateUser(UserUpdateRequestModel model);
        Task DeleteUser(Guid guid);
        Task<IEnumerable<UserResponseModel>> GetAllUsers();
        Task SetPermissionsToUser(UserPermissionSetRequestModel model);
    }
}