using ACL.Abstraction.Business;
using ACL.Abstraction.Data;
using ACL.Abstraction.MetaData;
using ACL.Business.Exceptions;
using ACL.Contracts.RequestModels;
using ACL.Contracts.ResponseModels;
using ACL.Entity.Entities;

namespace ACL.Business.Services
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
            user.SetPermissions(role.GetPermissions());
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
            model.Permissions = model.Permissions.Distinct().ToList();
            
            var user = await _userRepository.GetByGuid(model.Guid) ?? throw new UserDoesNotExistsException();
            var invalidPermissions = model.Permissions.Except(_permissionMeta.Permissions).ToList();
            if(invalidPermissions.Any()) throw new InvalidPermissionException(invalidPermissions);
            
            var permissionDependencies = _permissionMeta.ListPermissionsDependencies(model.Permissions);
            var permissionWithDependencies = model.Permissions.Union(permissionDependencies);
            
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
    }
}