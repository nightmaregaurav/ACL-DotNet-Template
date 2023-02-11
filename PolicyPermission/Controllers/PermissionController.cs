using Microsoft.AspNetCore.Mvc;
using PolicyPermission.Authorization;
using PolicyPermission.Types;

namespace PolicyPermission.Controllers
{
    [ApiController]
    [Route("/api/permissions")]
    public class PermissionController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(IDictionary<string,IEnumerable<string>>), 200)]
        public Task<IActionResult> Get()
        {
            return Task.FromResult<IActionResult>(Ok(RequirePermissionAttribute.GetPermissionMap()));
        }

        [HttpGet("{scope}")]
        [ProducesResponseType(typeof(IEnumerable<string>), 200)]
        public Task<IActionResult> Get(string scope)
        {
            return Task.FromResult<IActionResult>(Ok(RequirePermissionAttribute.ListPermissions(scope)));
        }

        [HttpGet("list")]
        [ProducesResponseType(typeof(IEnumerable<string>), 200)]
        [RequirePermission(Permission.ViewPolicy)]
        public Task<IActionResult> List()
        {
            return Task.FromResult<IActionResult>(Ok(RequirePermissionAttribute.ListPermissions()));
        }
        
        [HttpGet("list-scopes")]
        [ProducesResponseType(typeof(IEnumerable<string>), 200)]
        [RequirePermission(Permission.ViewPolicy)]
        public Task<IActionResult> ListScopes()
        {
            return Task.FromResult<IActionResult>(Ok(RequirePermissionAttribute.ListScopes()));
        }
        
        [HttpGet("dependencies")]
        [ProducesResponseType(typeof(IDictionary<string,IEnumerable<string>>), 200)]
        [RequirePermission(Permission.ViewPolicy)]
        public Task<IActionResult> Dependencies()
        {
            return Task.FromResult<IActionResult>(Ok(PermissionDependencies.GetDependencyMap()));
        }
        
        [HttpGet("{permission}/dependencies")]
        [ProducesResponseType(typeof(IDictionary<string,IEnumerable<string>>), 200)]
        [RequirePermission(Permission.ViewPolicy)]
        public Task<IActionResult> Dependencies(string permission)
        {
            return Task.FromResult<IActionResult>(Ok(PermissionDependencies.ListDependencies(permission)));
        }
    }
}