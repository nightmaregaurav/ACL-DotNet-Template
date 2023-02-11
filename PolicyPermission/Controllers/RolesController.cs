using Microsoft.AspNetCore.Mvc;
using PolicyPermission.Abstraction.Business;
using PolicyPermission.Contracts.RequestModels;
using PolicyPermission.Contracts.ResponseModels;

namespace PolicyPermission.Controllers
{
    [ApiController]
    [Route("/api/roles")]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<RoleResponseModel>), 200)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _roleService.GetAll());
        }

        [HttpGet("{guid}/permissions")]
        [ProducesResponseType(typeof(IEnumerable<string>), 200)]
        public async Task<IActionResult> GetPermissions(Guid guid)
        {
            return Ok(await _roleService.GetPermissions(guid));
        }

        [HttpPost]
        [ProducesResponseType(typeof(Guid), 200)]
        public async Task<IActionResult> Post(RoleAddRequestModel model)
        {
            return Ok(await _roleService.Add(model));
        }

        [HttpPut]
        [ProducesResponseType(typeof(void), 200)]
        public async Task<IActionResult> Put(RoleUpdateRequestModel model)
        {
            await _roleService.Update(model);
            return Ok();
        }

        [HttpDelete]
        [ProducesResponseType(typeof(void), 200)]
        public async Task<IActionResult> Delete(Guid guid)
        {
            await _roleService.Delete(guid);
            return Ok();
        }

        [HttpPatch]
        [ProducesResponseType(typeof(void), 200)]
        public async Task<IActionResult> Patch(RolePermissionSetRequestModel model)
        {
            await _roleService.SetPermissions(model);
            return Ok();
        }
    }
}