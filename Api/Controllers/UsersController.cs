using Api.Controllers.Base;
using Api.MetaData;
using Business.Abstraction.Services;
using Business.Models.RequestDto;
using Microsoft.AspNetCore.Mvc;
using Models.RequestModels;
using Models.ResponseModels;

namespace Api.Controllers
{
    [Route("/api/users")]
    public class UsersController(UserMeta userMeta, IUserService userService) : BaseApiController
    {
        [HttpGet("permissions")]
        [ProducesResponseType(typeof(UserPermissionResponseModel), 200)]
        public async Task<IActionResult> GetPermissions()
        {
            var permissionDto = await userService.GetPermissionsAsync(userMeta.Guid).ConfigureAwait(true);
            var permissionResponseModel = new UserPermissionResponseModel
            {
                DirectPermissions = permissionDto.DirectPermissions,
                InheritedPermissions = permissionDto.InheritedPermissions.Select(x => new InheritedPermissionModel
                {
                    Permissions = x.Permissions,
                    InheritedFromRoleGuid = x.InheritedFromRoleGuid
                }).ToList()
            };
            return Ok(permissionResponseModel);
        }

        [HttpGet("{guid}/permissions")]
        [ProducesResponseType(typeof(UserPermissionResponseModel), 200)]
        public async Task<IActionResult> GetPermissions(string guid)
        {
            var permissionDto = await userService.GetPermissionsAsync(guid).ConfigureAwait(true);
            var permissionResponseModel = new UserPermissionResponseModel
            {
                DirectPermissions = permissionDto.DirectPermissions,
                InheritedPermissions = permissionDto.InheritedPermissions.Select(x => new InheritedPermissionModel
                {
                    Permissions = x.Permissions,
                    InheritedFromRoleGuid = x.InheritedFromRoleGuid
                }).ToList()
            };
            return Ok(permissionResponseModel);
        }

        [HttpPatch("permissions")]
        [ProducesResponseType(typeof(UserPermissionResponseModel), 200)]
        public async Task<IActionResult> Patch(UserPermissionSetRequestModel model)
        {
            var dto = new UserPermissionSetRequestDto
            {
                Guid = model.Guid,
                Permissions = model.Permissions
            };

            var permissionDto = await userService.SetAndGetNewPermissionsAsync(dto).ConfigureAwait(true);
            var permissionResponseModel = new UserPermissionResponseModel
            {
                DirectPermissions = permissionDto.DirectPermissions,
                InheritedPermissions = permissionDto.InheritedPermissions.Select(x => new InheritedPermissionModel
                {
                    Permissions = x.Permissions,
                    InheritedFromRoleGuid = x.InheritedFromRoleGuid
                }).ToList()
            };
            return Ok(permissionResponseModel);
        }
    }
}
