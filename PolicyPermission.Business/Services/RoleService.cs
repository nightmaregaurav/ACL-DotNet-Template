using PolicyPermission.Abstraction.Business;
using PolicyPermission.Abstraction.Data;
using PolicyPermission.Business.Exceptions;
using PolicyPermission.Contracts.RequestModels;
using PolicyPermission.Contracts.ResponseModels;
using PolicyPermission.Entity.Entities;

namespace PolicyPermission.Business.Services
{
    internal class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<Guid> AddRole(RoleAddRequestModel model)
        {
            if (await IsRoleWithSameNameExists(model.Name)) throw new RoleAlreadyExistsException();
            var role = new Role(model.Name, model.Description);
            await _roleRepository.Add(role);
            return role.Guid;
        }

        public async Task UpdateRole(RoleUpdateRequestModel model)
        {
            var role = await GetRoleIfAnotherRoleWithSameNameDoesNotExists(model.Name, model.Guid) ??
                       await _roleRepository.GetByGuid(model.Guid) ?? throw new RoleDoesNotExistsException();
            role.Update(model.Name, model.Description);
            await _roleRepository.Update(role);
        }

        public async Task DeleteRole(Guid guid)
        {
            var role = await _roleRepository.GetByGuid(guid) ?? throw new RoleDoesNotExistsException();
            await _roleRepository.Remove(role);
        }

        public async Task<IEnumerable<RoleResponseModel>> GetAllRoles()
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

        public async Task SetPermissionsToRole(RolePermissionSetRequestModel model)
        {
            var role = await _roleRepository.GetByGuid(model.Guid) ?? throw new RoleDoesNotExistsException();
            role.SetPermissions(model.Permissions);
            await _roleRepository.Update(role);
        }

        public async Task<IEnumerable<string>> GetPermissions(Guid guid)
        {
            var role = await _roleRepository.GetByGuid(guid) ?? throw new RoleDoesNotExistsException();
            return role.GetPermissions();
        }

        private async Task<bool> IsRoleWithSameNameExists(string roleName)
        {
            var role = await _roleRepository.GetRoleByName(roleName);
            return role != null;
        }

        private async Task<Role?> GetRoleIfAnotherRoleWithSameNameDoesNotExists(string roleName, Guid guid)
        {
            var role = await _roleRepository.GetRoleByName(roleName);
            if (role == null) return null;

            if (role.Guid != guid) throw new RoleAlreadyExistsException();

            return role;
        }
    }
}