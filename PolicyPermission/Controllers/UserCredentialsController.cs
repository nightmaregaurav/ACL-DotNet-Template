using Microsoft.AspNetCore.Mvc;
using PolicyPermission.Abstraction.Business;
using PolicyPermission.Contracts.RequestModels;
using PolicyPermission.Contracts.ResponseModels;

namespace PolicyPermission.Controllers
{
    [ApiController]
    [Route("/api/credentials")]
    public class UserCredentialsController : ControllerBase
    {
        private readonly IUserCredentialService _credentialService;

        public UserCredentialsController(IUserCredentialService credentialService)
        {
            _credentialService = credentialService;
        }
        
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserCredentialResponseModel>), 200)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _credentialService.GetAllCredentials());
        }
        
        [HttpPost]
        [ProducesResponseType(typeof(Guid), 200)]
        public async Task<IActionResult> Post(UserCredentialAddRequestModel requestModel)
        {
            return Ok(await _credentialService.AddCredential(requestModel));
        }
        
        [HttpPut]
        [ProducesResponseType(typeof(void), 200)]
        public async Task<IActionResult> Put(UserCredentialUpdateRequestModel requestModel)
        {
            await _credentialService.UpdateCredential(requestModel);
            return Ok();
        }
        
        [HttpDelete]
        [ProducesResponseType(typeof(void), 200)]
        public async Task<IActionResult> Delete(Guid guid)
        {
            await _credentialService.DeleteCredential(guid);
            return Ok();
        }
    }
}