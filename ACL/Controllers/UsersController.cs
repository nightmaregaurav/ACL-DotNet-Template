using ACL.Abstraction.Business;
using ACL.Contracts.RequestModels;
using ACL.Contracts.ResponseModels;
using Microsoft.AspNetCore.Mvc;

namespace ACL.Controllers
{
    [ApiController]
    [Route("/api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserResponseModel>), 200)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _userService.GetAll());
        }

        [HttpGet("permissions")]
        [ProducesResponseType(typeof(IEnumerable<string>), 200)]
        public async Task<IActionResult> GetPermissions()
        {
            return Ok(await _userService.GetPermissions());
        }

        [HttpGet("{guid}/permissions")]
        [ProducesResponseType(typeof(IEnumerable<string>), 200)]
        public async Task<IActionResult> GetPermissions(Guid guid)
        {
            return Ok(await _userService.GetPermissions(guid));
        }

        [HttpPost]
        [ProducesResponseType(typeof(Guid), 200)]
        public async Task<IActionResult> Post(UserAddRequestModel model)
        {
            return Ok(await _userService.Add(model));
        }

        [HttpPut]
        [ProducesResponseType(typeof(void), 200)]
        public async Task<IActionResult> Put(UserUpdateRequestModel model)
        {
            await _userService.Update(model);
            return Ok();
        }

        [HttpDelete]
        [ProducesResponseType(typeof(void), 200)]
        public async Task<IActionResult> Delete(Guid guid)
        {
            await _userService.Delete(guid);
            return Ok();
        }

        [HttpPatch]
        [ProducesResponseType(typeof(IEnumerable<string>), 200)]
        public async Task<IActionResult> Patch(UserPermissionSetRequestModel model)
        {
            return Ok(await _userService.SetAndGetNewPermissions(model));
        }
    }
}