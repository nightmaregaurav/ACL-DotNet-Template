using Business.Contracts.RequestModels;

namespace Business.Abstraction.Services
{
    public interface IAuthenticationService
    {
        Task<string> Login(UserLoginRequestModel model);
    }
}
