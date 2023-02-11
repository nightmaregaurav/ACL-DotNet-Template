using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PolicyPermission.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/permissions")]
    public class PermissionController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> Get()
        {
            return Ok("WIP");
        }

        [HttpGet("{scope}")]
        [ProducesResponseType(typeof(IEnumerable<string>), 200)]
        public async Task<IActionResult> Get(string scope)
        {
            return Ok("WIP");
        }

        [HttpGet("list")]
        [ProducesResponseType(typeof(IEnumerable<string>), 200)]
        public async Task<IActionResult> List()
        {
            return Ok("WIP");
        }
    }
}