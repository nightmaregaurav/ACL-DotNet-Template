using PolicyPermission.Contracts.RequestModels;

namespace PolicyPermission.Abstraction.Business
{
    public interface IAuthenticationService
    {
        Task<string> Login(UserLoginRequestModel model);
    }
}