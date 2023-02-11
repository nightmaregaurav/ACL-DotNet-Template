using PolicyPermission.Contracts.RequestModels;
using PolicyPermission.Contracts.ResponseModels;

namespace PolicyPermission.Abstraction.Business
{
    public interface IRoleService
    {
        Task<Guid> Add(RoleAddRequestModel model);
        Task Update(RoleUpdateRequestModel model);
        Task Delete(Guid guid);
        Task<IEnumerable<RoleResponseModel>> GetAll();
        Task SetPermissions(RolePermissionSetRequestModel model);
        Task<IEnumerable<string>> GetPermissions(Guid guid);
    }
}