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
        Task SetPermissions(UserPermissionSetRequestModel model);
        Task<IEnumerable<string>> GetPermissions(Guid guid);
        Task<IEnumerable<string>> GetPermissions();
        Task<IEnumerable<string>> GetPermissionsInheritedFromRole(Guid guid);
        Task<IEnumerable<string>> GetPermissionsInheritedFromRole();
    }
}