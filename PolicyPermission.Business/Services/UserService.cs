using PolicyPermission.Abstraction.Business;
using PolicyPermission.Abstraction.Data;
using PolicyPermission.Abstraction.MetaData;
using PolicyPermission.Business.Exceptions;
using PolicyPermission.Contracts.RequestModels;
using PolicyPermission.Contracts.ResponseModels;
using PolicyPermission.Entity.Entities;

namespace PolicyPermission.Business.Services
{
    internal class UserService : IUserService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUserMeta _userMeta;
        private readonly IUserRepository _userRepository;
        private readonly IPermissionMeta _permissionMeta;

        public UserService(IUserMeta userMeta, IUserRepository userRepository, IRoleRepository roleRepository, IPermissionMeta permissionMeta)
        {
            _userMeta = userMeta;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _permissionMeta = permissionMeta;
        }

        public async Task<Guid> Add(UserAddRequestModel model)
        {
            var role = await _roleRepository.GetByGuid(model.Role) ?? throw new RoleDoesNotExistsException();
            var user = new User(model.FullName, role);
            await _userRepository.Add(user);
            return role.Guid;
        }

        public async Task Update(UserUpdateRequestModel model)
        {
            var user = await _userRepository.GetByGuid(model.Guid) ?? throw new UserDoesNotExistsException();
            var role = await _roleRepository.GetByGuid(model.Role) ?? throw new RoleDoesNotExistsException();
            user.Update(model.FullName, role);
            await _userRepository.Update(user);
        }

        public async Task Delete(Guid guid)
        {
            var user = await _userRepository.GetByGuid(guid) ?? throw new UserDoesNotExistsException();
            await _userRepository.Remove(user);
        }

        public async Task<IEnumerable<UserResponseModel>> GetAll()
        {
            var users = await _userRepository.GetAll();
            return users.Select(user => new UserResponseModel
            {
                Guid = user.Guid,
                FullName = user.FullName,
                Role = user.Role.Name,
                Permissions = user.GetPermissions().Concat(user.Role.GetPermissions()).Distinct()
            });
        }

        public async Task<IEnumerable<string>> SetAndGetNewPermissions(UserPermissionSetRequestModel model)
        {
            var user = await _userRepository.GetByGuid(model.Guid) ?? throw new UserDoesNotExistsException();
            var invalidPermissions = model.Permissions.Except(_permissionMeta.Permissions).ToList();
            if(invalidPermissions.Any()) throw new InvalidPermissionException(invalidPermissions);
            var permissionWithDependencies = GetPermissionsWithDependencies(model.Permissions);
            user.SetPermissions(permissionWithDependencies);
            await _userRepository.Update(user);
            return user.GetPermissions();
        }

        public async Task<IEnumerable<string>> GetPermissions()
        {
            return await GetPermissions(_userMeta.Guid);
        }

        public async Task<IEnumerable<string>> GetPermissions(Guid guid)
        {
            var user = await _userRepository.GetByGuid(guid) ?? throw new UserDoesNotExistsException();
            return user.GetPermissions();
        }

        public async Task<IEnumerable<string>> GetPermissionsInheritedFromRole()
        {
            return await GetPermissionsInheritedFromRole(_userMeta.Guid);
        }

        public async Task<IEnumerable<string>> GetPermissionsInheritedFromRole(Guid guid)
        {
            var user = await _userRepository.GetByGuid(guid) ?? throw new UserDoesNotExistsException();
            return user.Role.GetPermissions();
        }

        public async Task<IEnumerable<string>> GetAllPermissions()
        {
            return await GetAllPermissions(_userMeta.Guid);
        }

        public async Task<IEnumerable<string>> GetAllPermissions(Guid guid)
        {
            var user = await _userRepository.GetByGuid(guid) ?? throw new UserDoesNotExistsException();
            return user.GetPermissions().Concat(user.Role.GetPermissions()).Distinct();
        }
        
        private IEnumerable<string> GetPermissionsWithDependencies(IEnumerable<string> permissions)
        {
            var permissionsWithDependencies = new List<string>();

            int permissionCount = 0;
            foreach (var permission in permissions)
            {
                permissionCount++;
                permissionsWithDependencies.Add(permission);
                
                var dependencies = _permissionMeta.ListDependencies(permission);
                permissionsWithDependencies.AddRange(dependencies);
            }
            var permissionSet = permissionsWithDependencies.Distinct().ToList();
            
            return permissionSet.Count == permissionCount ? permissionSet : GetPermissionsWithDependencies(permissionSet);
        }
    }
}