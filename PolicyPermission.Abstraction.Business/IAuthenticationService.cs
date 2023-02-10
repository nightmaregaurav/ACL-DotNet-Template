namespace PolicyPermission.Abstraction.Business
{
    public interface IAuthenticationService
    {
        Task<string> Login(string userName, string password);
    }
}