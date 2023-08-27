using Business.Contracts.RequestModels;
using Business.Contracts.ResponseModels;

namespace Business.Abstraction.Services
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