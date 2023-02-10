using Microsoft.AspNetCore.Mvc;
using PolicyPermission.Abstraction.Business;

namespace PolicyPermission.Controllers
{
    public class LoginController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public LoginController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        // public Task<IActionResult> GetToken()
        // {
        //     
        // }
        [HttpPost]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> Login([FromBody] string userName, [FromBody] string password)
        {
            var token = await _authenticationService.Login(userName, password);
            return Ok(token);
        }
    }
}