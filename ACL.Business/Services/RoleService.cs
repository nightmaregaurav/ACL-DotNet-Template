using ACL.Abstraction.Business;
using ACL.Abstraction.Data;
using ACL.Abstraction.MetaData;
using ACL.Business.Exceptions;
using ACL.Contracts.RequestModels;
using ACL.Contracts.ResponseModels;
using ACL.Entity.Entities;

namespace ACL.Business.Services
{
    internal class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IPermissionMeta _permissionMeta;

        public RoleService(IRoleRepository roleRepository, IPermissionMeta permissionMeta)
        {
            _roleRepository = roleRepository;
            _permissionMeta = permissionMeta;
        }

        public async Task<Guid> Add(RoleAddRequestModel model)
        {
            if (await IsRoleWithSameNameExists(model.Name)) throw new RoleAlreadyExistsException();
            var role = new Role(model.Name, model.Description);
            await _roleRepository.Add(role);
            return role.Guid;
        }

        public async Task Update(RoleUpdateRequestModel model)
        {
            var role = await GetRoleIfAnotherRoleWithSameNameDoesNotExists(model.Name, model.Guid) ??
                       await _roleRepository.GetByGuid(model.Guid) ?? throw new RoleDoesNotExistsException();
            role.Update(model.Name, model.Description);
            await _roleRepository.Update(role);
        }

        public async Task Delete(Guid guid)
        {
            var role = await _roleRepository.GetByGuid(guid) ?? throw new RoleDoesNotExistsException();
            await _roleRepository.Remove(role);
        }

        public async Task<IEnumerable<RoleResponseModel>> GetAll()
        {
            var roles = await _roleRepository.GetAll();
            return roles.Select(role => new RoleResponseModel
            {
                Guid = role.Guid,
                Name = role.Name,
                Description = role.Description,
                Permissions = role.GetPermissions()
            });
        }

        public async Task<IEnumerable<string>> SetAndGetNewPermissions(RolePermissionSetRequestModel model)
        {
            model.Permissions = model.Permissions.Distinct().ToList();
            
            var role = await _roleRepository.GetByGuid(model.Guid) ?? throw new RoleDoesNotExistsException();
            var invalidPermissions = model.Permissions.Except(_permissionMeta.Permissions).ToList();
            if(invalidPermissions.Any()) throw new InvalidPermissionException(invalidPermissions);
            
            var permissionsDependencies = _permissionMeta.ListPermissionsDependencies(model.Permissions);
            var permissionsWithDependencies = model.Permissions.Union(permissionsDependencies);
            
            role.SetPermissions(permissionsWithDependencies);
            await _roleRepository.Update(role);
            
            return role.GetPermissions();
        }

        public async Task<IEnumerable<string>> GetPermissions(Guid guid)
        {
            var role = await _roleRepository.GetByGuid(guid) ?? throw new RoleDoesNotExistsException();
            return role.GetPermissions();
        }

        private async Task<bool> IsRoleWithSameNameExists(string roleName)
        {
            var role = await _roleRepository.GetByName(roleName);
            return role != null;
        }

        private async Task<Role?> GetRoleIfAnotherRoleWithSameNameDoesNotExists(string roleName, Guid guid)
        {
            var role = await _roleRepository.GetByName(roleName);
            if (role == null) return null;

            if (role.Guid != guid) throw new RoleAlreadyExistsException();

            return role;
        }
    }
}