using Business.Models.RequestDto;
using Business.Models.ResponseDto;

namespace Business.Abstraction.Services
{
    public interface IUserService
    {
        Task<UserPermissionResponseDto> SetAndGetNewPermissionsAsync(UserPermissionSetRequestDto dto);
        Task<UserPermissionResponseDto> GetPermissionsAsync(string guid);
        Task BulkUpdateLastSeenAsync(Dictionary<string, DateTime> userGuidDict);
    }
}
