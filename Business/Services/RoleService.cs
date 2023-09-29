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
            
            var role = await roleRepository.GetByGuid(dto.Guid) ?? throw new RoleDoesNotExistsException();
            var invalidPermissions = dto.Permissions.Except(permissionHelper.Permissions).ToList();
            if(invalidPermissions.Count != 0) throw new InvalidPermissionException(invalidPermissions);
            
            var permissionsWithDependencies = permissionHelper.ListPermissionsWithDependencies(dto.Permissions.ToArray());

            role.SetPermissions(permissionsWithDependencies);
            await roleRepository.Update(role);
            
            return role.GetPermissions();
        }

        public async Task<IEnumerable<string>> GetPermissionsAsync(string guid)
        {
            var role = await roleRepository.GetByGuid(guid) ?? throw new RoleDoesNotExistsException();
            return role.GetPermissions();
        }
    }
}
