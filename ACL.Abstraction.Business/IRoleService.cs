using ACL.Contracts.RequestModels;
using ACL.Contracts.ResponseModels;

namespace ACL.Abstraction.Business
{
    public interface IRoleService
    {
        Task<Guid> Add(RoleAddRequestModel model);
        Task Update(RoleUpdateRequestModel model);
        Task Delete(Guid guid);
        Task<IEnumerable<RoleResponseModel>> GetAll();
        Task<IEnumerable<string>> SetAndGetNewPermissions(RolePermissionSetRequestModel model);
        Task<IEnumerable<string>> GetPermissions(Guid guid);
    }
}