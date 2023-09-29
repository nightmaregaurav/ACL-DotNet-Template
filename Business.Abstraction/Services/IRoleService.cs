using Business.Models.RequestDto;

namespace Business.Abstraction.Services
{
    public interface IRoleService
    {
        Task<IEnumerable<string>> SetAndGetNewPermissionsAsync(RolePermissionSetRequestDto dto);
        Task<IEnumerable<string>> GetPermissionsAsync(string guid);
    }
}
