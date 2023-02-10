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
            return Ok(await _roleService.GetAllRoles());
        }
        
        [HttpPost]
        [ProducesResponseType(typeof(Guid), 200)]
        public async Task<IActionResult> Post(RoleAddRequestModel model)
        {
            return Ok(await _roleService.AddRole(model));
        }
        
        [HttpPut]
        [ProducesResponseType(typeof(void), 200)]
        public async Task<IActionResult> Put(RoleUpdateRequestModel model)
        {
            await _roleService.UpdateRole(model);
            return Ok();
        }
        
        [HttpDelete]
        [ProducesResponseType(typeof(void), 200)]
        public async Task<IActionResult> Delete(Guid guid)
        {
            await _roleService.DeleteRole(guid);
            return Ok();
        }
    }
}