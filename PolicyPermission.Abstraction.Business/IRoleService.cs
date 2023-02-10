using PolicyPermission.Contracts.RequestModels;
using PolicyPermission.Contracts.ResponseModels;

namespace PolicyPermission.Abstraction.Business
{
    public interface IRoleService
    {
        Task<Guid> AddRole(RoleAddRequestModel model);
        Task UpdateRole(RoleUpdateRequestModel model);
        Task DeleteRole(Guid guid);
        Task<IEnumerable<RoleResponseModel>> GetAllRoles();
        Task SetPermissionsToRole(RolePermissionSetRequestModel model);
    }
}