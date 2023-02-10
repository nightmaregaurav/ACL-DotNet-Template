using PolicyPermission.Abstraction.Business;
using PolicyPermission.Abstraction.Data;
using PolicyPermission.Business.Exceptions;
using PolicyPermission.Contracts.RequestModels;
using PolicyPermission.Contracts.ResponseModels;
using PolicyPermission.Entity.Entities;

namespace PolicyPermission.Business.Services
{
    internal class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public UserService(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }
        
        public async Task<Guid> AddUser(UserAddRequestModel model)
        {
            var role = await _roleRepository.GetByGuid(model.Role) ?? throw new RoleDoesNotExistsException();
            var user = new User(model.FullName, role);
            await _userRepository.Add(user);
            return role.Guid;
        }

        public async Task UpdateUser(UserUpdateRequestModel model)
        {
            var user = await _userRepository.GetByGuid(model.Guid) ?? throw new UserDoesNotExistsException();
            var role = await _roleRepository.GetByGuid(model.Role) ?? throw new RoleDoesNotExistsException();
            user.Update(model.FullName, role);
            _userRepository.Update(user);
        }

        public async Task DeleteUser(Guid guid)
        {
            var user = await _userRepository.GetByGuid(guid) ?? throw new UserDoesNotExistsException();
            _userRepository.Remove(user);
        }

        public async Task<IEnumerable<UserResponseModel>> GetAllUsers()
        {
            var users = await _userRepository.GetAll();
            return users.Select(user => new UserResponseModel
            {
                Guid = user.Guid,
                FullName = user.FullName,
                Role = user.Role.Name
            });
        }
    }
}