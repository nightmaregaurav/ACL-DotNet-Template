using Business.Contracts.RequestModels;

namespace Business.Abstraction
{
    public interface IAuthenticationService
    {
        Task<string> Login(UserLoginRequestModel model);
    }
}