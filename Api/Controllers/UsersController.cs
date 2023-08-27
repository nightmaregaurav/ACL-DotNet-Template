using Api.MetaData;
using Business.Abstraction.Services;
using Business.Contracts.RequestModels;
using Business.Contracts.ResponseModels;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("/api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly UserMeta _userMeta;

        public UsersController(IUserService userService, UserMeta userMeta)
        {
            _userService = userService;
            _userMeta = userMeta;
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
            return Ok(await _userService.GetPermissions(_userMeta.Guid));
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
