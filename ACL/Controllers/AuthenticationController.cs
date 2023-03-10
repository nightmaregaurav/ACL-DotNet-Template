using ACL.Abstraction.Business;
using ACL.Contracts.RequestModels;
using Microsoft.AspNetCore.Mvc;

namespace ACL.Controllers
{
    [ApiController]
    [Route("/api/auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        
        [HttpPost]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> Login(UserLoginRequestModel model)
        {
            var token = await _authenticationService.Login(model);
            return Ok(token);
        }
    }
}