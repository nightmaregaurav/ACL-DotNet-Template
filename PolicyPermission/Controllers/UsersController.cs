using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PolicyPermission.Abstraction.Business;
using PolicyPermission.Contracts.RequestModels;
using PolicyPermission.Contracts.ResponseModels;

namespace PolicyPermission.Controllers
{
    [Authorize]
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
            return Ok(await _userService.GetAllUsers());
        }

        [HttpGet("permissions")]
        [ProducesResponseType(typeof(IEnumerable<string>), 200)]
        public async Task<IActionResult> GetPermissions()
        {
            return Ok(await _userService.GetPermissions());
        }

        [HttpGet("role-permissions")]
        [ProducesResponseType(typeof(IEnumerable<string>), 200)]
        public async Task<IActionResult> GetRolePermissions()
        {
            return Ok(await _userService.GetPermissionsInheritedFromRole());
        }

        [HttpGet("{guid}/permissions")]
        [ProducesResponseType(typeof(IEnumerable<string>), 200)]
        public async Task<IActionResult> GetPermissions(Guid guid)
        {
            return Ok(await _userService.GetPermissions(guid));
        }

        [HttpGet("{guid}/role-permissions")]
        [ProducesResponseType(typeof(IEnumerable<string>), 200)]
        public async Task<IActionResult> GetRolePermissions(Guid guid)
        {
            return Ok(await _userService.GetPermissionsInheritedFromRole(guid));
        }

        [HttpPost]
        [ProducesResponseType(typeof(Guid), 200)]
        public async Task<IActionResult> Post(UserAddRequestModel model)
        {
            return Ok(await _userService.AddUser(model));
        }

        [HttpPut]
        [ProducesResponseType(typeof(void), 200)]
        public async Task<IActionResult> Put(UserUpdateRequestModel model)
        {
            await _userService.UpdateUser(model);
            return Ok();
        }

        [HttpDelete]
        [ProducesResponseType(typeof(void), 200)]
        public async Task<IActionResult> Delete(Guid guid)
        {
            await _userService.DeleteUser(guid);
            return Ok();
        }

        [HttpPatch]
        [ProducesResponseType(typeof(void), 200)]
        public async Task<IActionResult> Patch(UserPermissionSetRequestModel model)
        {
            await _userService.SetPermissions(model);
            return Ok();
        }
    }
}