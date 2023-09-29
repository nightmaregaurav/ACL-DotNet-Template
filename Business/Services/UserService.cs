using Business.Abstraction.Helpers;
using Business.Abstraction.Services;
using Business.Exceptions;
using Business.Models.RequestDto;
using Business.Models.ResponseDto;
using Data.Abstraction.Repositories;

namespace Business.Services
{
    internal class UserService(IUserRepository userRepository, IPermissionHelper permissionHelper) : IUserService
    {
        public async Task<UserPermissionResponseDto> SetAndGetNewPermissionsAsync(UserPermissionSetRequestDto dto)
        {
            dto.Permissions = dto.Permissions.Distinct().ToList();
            
            var user = await userRepository.GetByGuid(dto.Guid) ?? throw new UserDoesNotExistsException();
            var invalidPermissions = dto.Permissions.Except(permissionHelper.Permissions).ToList();
            if(invalidPermissions.Count != 0) throw new InvalidPermissionException(invalidPermissions);
            
            var permissionWithDependencies = permissionHelper.ListPermissionsWithDependencies(dto.Permissions.ToArray());

            user.SetPermissions(permissionWithDependencies);
            await userRepository.Update(user);
            
            var directPermissions = user.GetPermissions().ToList();
            var inheritedPermissions = user.Roles.Select(role => new InheritedPermissionDto
            {
                Permissions = role.GetPermissions().ToList(),
                InheritedFromRoleId = role.Id
            }).ToList();

            return new UserPermissionResponseDto
            {
                DirectPermissions = directPermissions,
                InheritedPermissions = inheritedPermissions
            };
        }

        public async Task<UserPermissionResponseDto> GetPermissionsAsync(string guid)
        {
            var user = await userRepository.GetByGuid(guid) ?? throw new UserDoesNotExistsException();
            var directPermissions = user.GetPermissions().ToList();
            var inheritedPermissions = user.Roles.Select(role => new InheritedPermissionDto
            {
                Permissions = role.GetPermissions().ToList(),
                InheritedFromRoleId = role.Id
            }).ToList();

            return new UserPermissionResponseDto
            {
                DirectPermissions = directPermissions,
                InheritedPermissions = inheritedPermissions
            };
        }

        public async Task BulkUpdateLastSeenAsync(Dictionary<string, DateTime> userGuidDict)
        {
            throw new NotImplementedException();
        }
    }
}
