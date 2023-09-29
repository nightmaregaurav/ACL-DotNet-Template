using Api.Controllers.Base;
using Api.MetaData;
using Business.Abstraction.Services;
using Business.Models.RequestDto;
using Microsoft.AspNetCore.Mvc;
using Models.RequestModels;

namespace Api.Controllers
{
    [Route("/api/roles")]
    public class RolesController(UserMeta userMeta, IRoleService roleService) : BaseApiController
    {
        [HttpGet("permissions")]
        [ProducesResponseType(typeof(IEnumerable<string>), 200)]
        public async Task<IActionResult> GetPermissions()
        {
            return Ok(await roleService.GetPermissionsAsync(userMeta.Guid));
        }

        [HttpGet("{guid}/permissions")]
        [ProducesResponseType(typeof(IEnumerable<string>), 200)]
        public async Task<IActionResult> GetPermissions(string guid)
        {
            return Ok(await roleService.GetPermissionsAsync(guid));
        }

        [HttpPatch("permissions")]
        [ProducesResponseType(typeof(IEnumerable<string>), 200)]
        public async Task<IActionResult> Patch(RolePermissionSetRequestModel model)
        {
            var dto = new RolePermissionSetRequestDto
            {
                Guid = model.Guid,
                Permissions = model.Permissions
            };
            return Ok(await roleService.SetAndGetNewPermissionsAsync(dto));
        }
    }
}
