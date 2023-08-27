using Business.Abstraction.Helpers;
using Business.Abstraction.Services;
using Business.Contracts.RequestModels;
using Business.Contracts.ResponseModels;
using Business.Exceptions;
using Data.Abstraction.Repositories;
using Data.Entity.Entities;

namespace Business.Services
{
    internal class UserService : IUserService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPermissionHelper _permissionHelper;

        public UserService(IUserRepository userRepository, IRoleRepository roleRepository, IPermissionHelper permissionHelper)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _permissionHelper = permissionHelper;
        }

        public async Task<Guid> Add(UserAddRequestModel model)
        {
            var role = await _roleRepository.GetByGuid(model.Role) ?? throw new RoleDoesNotExistsException();
            var user = new User(model.FullName, role);
            user.SetPermissions(role.GetPermissions());
            await _userRepository.Insert(user);
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
            await _userRepository.Delete(user);
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
            var invalidPermissions = model.Permissions.Except(_permissionHelper.Permissions).ToList();
            if(invalidPermissions.Any()) throw new InvalidPermissionException(invalidPermissions);
            
            var permissionWithDependencies = _permissionHelper.ListPermissionsWithDependencies(model.Permissions.ToArray());

            user.SetPermissions(permissionWithDependencies);
            await _userRepository.Update(user);
            
            return user.GetPermissions();
        }

        public async Task<IEnumerable<string>> GetPermissions(Guid guid)
        {
            var user = await _userRepository.GetByGuid(guid) ?? throw new UserDoesNotExistsException();
            return user.GetPermissions();
        }
    }
}
