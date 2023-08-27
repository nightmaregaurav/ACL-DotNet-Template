using Api.ACL;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("/api/permissions")]
    public class PermissionController : ControllerBase
    {
        private readonly PermissionHelper _permissionHelper;

        public PermissionController(PermissionHelper permissionHelper)
        {
            _permissionHelper = permissionHelper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<string>), 200)]
        public Task<IActionResult> Get()
        {
            return Task.FromResult<IActionResult>(Ok(_permissionHelper.Permissions));
        }
        
        [HttpGet("dependencies")]
        [ProducesResponseType(typeof(IDictionary<string,IEnumerable<string>>), 200)]
        public Task<IActionResult> Dependencies()
        {
            return Task.FromResult<IActionResult>(Ok(_permissionHelper.PermissionDependencyMap));
        }
        
        [HttpGet("{permission}/dependencies")]
        [ProducesResponseType(typeof(IEnumerable<string>), 200)]
        public Task<IActionResult> Dependencies(string permission)
        {
            return Task.FromResult<IActionResult>(Ok(_permissionHelper.ListPermissionsWithDependencies(permission)));
        }
    }
}
