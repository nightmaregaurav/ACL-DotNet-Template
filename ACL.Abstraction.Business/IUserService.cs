using ACL.Contracts.RequestModels;
using ACL.Contracts.ResponseModels;

namespace ACL.Abstraction.Business
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