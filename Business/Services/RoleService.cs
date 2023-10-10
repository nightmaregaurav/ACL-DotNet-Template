using Business.Abstraction.Helpers;
using Business.Abstraction.Services;
using Business.Exceptions;
using Business.Models.RequestDto;
using Data.Abstraction.Repositories;

namespace Business.Services
{
    internal class RoleService(IRoleRepository roleRepository, IPermissionHelper permissionHelper) : IRoleService
    {
        public async Task<IEnumerable<string>> SetAndGetNewPermissionsAsync(RolePermissionSetRequestDto dto)
        {
            dto.Permissions = dto.Permissions.Distinct().ToList();
            
            var role = await roleRepository.GetByGuidAsync(dto.Guid).ConfigureAwait(false) ?? throw new RoleDoesNotExistsException();
            var invalidPermissions = dto.Permissions.Except(permissionHelper.Permissions).ToList();
            if(invalidPermissions.Count != 0) throw new InvalidPermissionException(invalidPermissions);
            
            var permissionsWithDependencies = permissionHelper.ListPermissionsWithDependencies(dto.Permissions.ToArray());

            role.SetPermissions(permissionsWithDependencies);
            await roleRepository.UpdateAsync(role).ConfigureAwait(false);
            
            return role.GetPermissionList();
        }

        public async Task<IEnumerable<string>> GetPermissionsAsync(string guid)
        {
            var role = await roleRepository.GetByGuidAsync(guid).ConfigureAwait(false) ?? throw new RoleDoesNotExistsException();
            return role.GetPermissionList();
        }
    }
}
