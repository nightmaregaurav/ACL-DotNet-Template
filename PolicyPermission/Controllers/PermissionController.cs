using Microsoft.AspNetCore.Mvc;
using PolicyPermission.Abstraction.MetaData;
using PolicyPermission.Authorization;
using PolicyPermission.Types;

namespace PolicyPermission.Controllers
{
    [ApiController]
    [Route("/api/permissions")]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionMeta _permissionMeta;

        public PermissionController(IPermissionMeta permissionMeta)
        {
            _permissionMeta = permissionMeta;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IDictionary<string,IEnumerable<string>>), 200)]
        public Task<IActionResult> Get()
        {
            return Task.FromResult<IActionResult>(Ok(_permissionMeta.PermissionScopeMap));
        }

        [HttpGet("{scope}")]
        [ProducesResponseType(typeof(IEnumerable<string>), 200)]
        public Task<IActionResult> Get(string scope)
        {
            return Task.FromResult<IActionResult>(Ok(_permissionMeta.ListPermissions(scope)));
        }

        [HttpGet("list")]
        [ProducesResponseType(typeof(IEnumerable<string>), 200)]
        [RequirePermission(Permission.Admin__ViewPolicy)]
        public Task<IActionResult> List()
        {
            return Task.FromResult<IActionResult>(Ok(_permissionMeta.Permissions));
        }
        
        [HttpGet("list-scopes")]
        [ProducesResponseType(typeof(IEnumerable<string>), 200)]
        [RequirePermission(Permission.Admin__ViewPolicy)]
        public Task<IActionResult> ListScopes()
        {
            return Task.FromResult<IActionResult>(Ok(_permissionMeta.Scopes));
        }
        
        [HttpGet("dependencies")]
        [ProducesResponseType(typeof(IDictionary<string,IEnumerable<string>>), 200)]
        [RequirePermission(Permission.Admin__ViewPolicy)]
        public Task<IActionResult> Dependencies()
        {
            return Task.FromResult<IActionResult>(Ok(_permissionMeta.PermissionDependencyMap));
        }
        
        [HttpGet("{permission}/dependencies")]
        [ProducesResponseType(typeof(IEnumerable<string>), 200)]
        [RequirePermission(Permission.Admin__ViewPolicy)]
        public Task<IActionResult> Dependencies(string permission)
        {
            return Task.FromResult<IActionResult>(Ok(_permissionMeta.ListDependencies(permission)));
        }
    }
}