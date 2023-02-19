using ACL.Contracts.RequestModels;

namespace ACL.Abstraction.Business
{
    public interface IAuthenticationService
    {
        Task<string> Login(UserLoginRequestModel model);
    }
}