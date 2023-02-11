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

        public UserService(IUserMeta userMeta, IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userMeta = userMeta;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
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

        public async Task SetPermissions(UserPermissionSetRequestModel model)
        {
            var user = await _userRepository.GetByGuid(model.Guid) ?? throw new UserDoesNotExistsException();
            user.SetPermissions(model.Permissions);
            await _userRepository.Update(user);
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
    }
}