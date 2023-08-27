using System.Security.Claims;
using Business.Abstraction.MetaData;
using Business.Contracts.RequestModels;
using Business.Exceptions;
using Business.Helpers;
using Data.Abstraction.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace Business.Services
{
    internal class AuthenticationService : Abstraction.Services.IAuthenticationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserCredentialRepository _userCredentialRepository;
        private readonly IJwtMeta _jwtMeta;

        public AuthenticationService(IHttpContextAccessor httpContextAccessor, IUserCredentialRepository userCredentialRepository, IJwtMeta jwtMeta)
        {
            _httpContextAccessor = httpContextAccessor;
            _userCredentialRepository = userCredentialRepository;
            _jwtMeta = jwtMeta;
        }
        
        public async Task<string> Login(UserLoginRequestModel model)
        {
            var credential = await _userCredentialRepository.GetByUsername(model.Username) ?? throw new CredentialDoesNotExistsException();
            var isValidHash = PasswordHelper.Validate(model.Username, model.Password, credential.Password);

            if (!isValidHash) throw new InvalidCredentialsException();
            
            var claims = new List<Claim>
            {
                new("jti", Guid.NewGuid().ToString()),
                new("iat", DateTimeOffset.Now.ToUnixTimeSeconds().ToString()),
                new("uid", credential.User.Guid.ToString())
            };

            var token = JwtTokenGenerationHelper.CreateToken(claims, _jwtMeta);
            
            var identity = new ClaimsIdentity(claims, "Basic");
            if(_httpContextAccessor.HttpContext != null) await _httpContextAccessor.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity),
                new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.Now.AddHours(10),
                    IsPersistent = true
                }
            );
            
            return token;
        }
    }
}