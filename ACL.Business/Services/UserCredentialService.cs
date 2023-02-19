using ACL.Abstraction.Business;
using ACL.Abstraction.Data;
using ACL.Business.Exceptions;
using ACL.Business.Helpers;
using ACL.Contracts.RequestModels;
using ACL.Contracts.ResponseModels;
using ACL.Entity.Entities;

namespace ACL.Business.Services
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

        public async Task<Guid> Add(UserCredentialAddRequestModel model)
        {
            if (await IsCredentialWithSameNameExists(model.Username)) throw new CredentialAlreadyExistsException();
            var user = await _userRepository.GetByGuid(model.User) ?? throw new UserDoesNotExistsException();

            var passwordHash = PasswordHelper.CreateHash(model.Username, model.Password);
            var credential = new UserCredential(model.Username, passwordHash, user);
            await _credentialRepository.Add(credential);
            return credential.Guid;
        }

        public async Task Update(UserCredentialUpdateRequestModel model)
        {
            var credential =
                await GetCredentialIfAnotherCredentialWithSameUsernameDoesNotExists(model.Username, model.Guid) ??
                await _credentialRepository.GetByGuid(model.Guid) ?? throw new CredentialDoesNotExistsException();
            credential.Update(model.Username, model.Password);
            await _credentialRepository.Update(credential);
        }

        public async Task Delete(Guid guid)
        {
            var credential = await _credentialRepository.GetByGuid(guid) ??
                             throw new CredentialDoesNotExistsException();
            await _credentialRepository.Remove(credential);
        }

        public async Task<IEnumerable<UserCredentialResponseModel>> GetAll()
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
            var credential = await _credentialRepository.GetByUsername(userName);
            return credential != null;
        }

        private async Task<UserCredential?> GetCredentialIfAnotherCredentialWithSameUsernameDoesNotExists(
            string userName, Guid guid)
        {
            var credential = await _credentialRepository.GetByUsername(userName);
            if (credential == null) return null;

            if (credential.Guid != guid) throw new CredentialAlreadyExistsException();

            return credential;
        }
    }
}