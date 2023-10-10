using Api.ACL;
using Api.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("/api/permissions")]
    public class PermissionController(PermissionHelper permissionHelper) : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<string>), 200)]
        public Task<IActionResult> Get() => Task.FromResult<IActionResult>(Ok(permissionHelper.Permissions));

        [HttpGet("dependencies")]
        [ProducesResponseType(typeof(IDictionary<string,IEnumerable<string>>), 200)]
        public Task<IActionResult> Dependencies() => Task.FromResult<IActionResult>(Ok(permissionHelper.PermissionDependencyMap));

        [HttpGet("incompatible-dependencies")]
        [ProducesResponseType(typeof(IDictionary<string,IEnumerable<string>>), 200)]
        public Task<IActionResult> IncompatibleDependencies() => Task.FromResult<IActionResult>(Ok(permissionHelper.IncompatiblePermissionMap));

        [HttpGet("{permission}/dependencies")]
        [ProducesResponseType(typeof(IEnumerable<string>), 200)]
        public Task<IActionResult> Dependencies(string permission) => Task.FromResult<IActionResult>(Ok(permissionHelper.ListPermissionsWithDependencies(permission)));
    }
}
