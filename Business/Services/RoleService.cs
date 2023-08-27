using Business.Abstraction.Helpers;
using Business.Abstraction.Services;
using Business.Contracts.RequestModels;
using Business.Contracts.ResponseModels;
using Business.Exceptions;
using Data.Abstraction.Repositories;
using Data.Entity.Entities;

namespace Business.Services
{
    internal class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IPermissionHelper _permissionHelper;

        public RoleService(IRoleRepository roleRepository, IPermissionHelper permissionHelper)
        {
            _roleRepository = roleRepository;
            _permissionHelper = permissionHelper;
        }

        public async Task<Guid> Add(RoleAddRequestModel model)
        {
            if (await IsRoleWithSameNameExists(model.Name)) throw new RoleAlreadyExistsException();
            var role = new Role(model.Name, model.Description);
            await _roleRepository.Insert(role);
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
            await _roleRepository.Delete(role);
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
            var invalidPermissions = model.Permissions.Except(_permissionHelper.Permissions).ToList();
            if(invalidPermissions.Any()) throw new InvalidPermissionException(invalidPermissions);
            
            var permissionsWithDependencies = _permissionHelper.ListPermissionsWithDependencies(model.Permissions.ToArray());

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
