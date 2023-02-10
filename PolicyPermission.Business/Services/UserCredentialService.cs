using PolicyPermission.Abstraction.Business;
using PolicyPermission.Abstraction.Data;
using PolicyPermission.Business.Exceptions;
using PolicyPermission.Contracts.RequestModels;
using PolicyPermission.Contracts.ResponseModels;
using PolicyPermission.Entity.Entities;

namespace PolicyPermission.Business.Services
{
    public class UserCredentialService : IUserCredentialService
    {
        private readonly IUserCredentialRepository _credentialRepository;
        private readonly IUserRepository _userRepository;

        public UserCredentialService(IUserCredentialRepository userCredentialRepository, IUserRepository userRepository)
        {
            _credentialRepository = userCredentialRepository;
            _userRepository = userRepository;
        }
        
        public async Task<Guid> AddCredential(UserCredentialAddRequestModel model)
        {
            if (await IsCredentialWithSameNameExists(model.Username)) throw new CredentialAlreadyExistsException();
            var user = await _userRepository.GetByGuid(model.User) ?? throw new UserDoesNotExistsException();
            var credential = new UserCredential(model.Username, model.Password, user);
            await _credentialRepository.Add(credential);
            return credential.Guid;
        }

        public async Task UpdateCredential(UserCredentialUpdateRequestModel model)
        {
            var credential = await GetCredentialIfAnotherCredentialWithSameUsernameDoesNotExists(model.Username, model.Guid) ?? await _credentialRepository.GetByGuid(model.Guid) ?? throw new CredentialDoesNotExistsException();
            credential.Update(model.Username, model.Password);
            _credentialRepository.Update(credential);
        }

        public async Task DeleteCredential(Guid guid)
        {
            var credential = await _credentialRepository.GetByGuid(guid) ?? throw new CredentialDoesNotExistsException();
            _credentialRepository.Remove(credential);
        }

        public async Task<IEnumerable<UserCredentialResponseModel>> GetAllCredentials()
        {
            var credentials = await _credentialRepository.GetAll();
            return credentials.Select(x => new UserCredentialResponseModel
            {
                Guid = x.Guid,
                User = x.User.Guid,
                Username = x.UserName,
                PasswordHash = x.Password
            });
        }
        
        private async Task<bool> IsCredentialWithSameNameExists(string userName)
        {
            var credential = await _credentialRepository.GetCredentialByUsername(userName);
            return credential != null;
        }
        
        private async Task<UserCredential?> GetCredentialIfAnotherCredentialWithSameUsernameDoesNotExists(string userName, Guid guid)
        {
            var credential = await _credentialRepository.GetCredentialByUsername(userName);
            if (credential == null) return null;
            
            if (credential.Guid != guid) throw new CredentialAlreadyExistsException();
            
            return credential;
        }
    }
}