using ACL.Abstraction.Business;
using ACL.Contracts.RequestModels;
using ACL.Contracts.ResponseModels;
using Microsoft.AspNetCore.Mvc;

namespace ACL.Controllers
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
            return Ok(await _credentialService.GetAll());
        }

        [HttpPost]
        [ProducesResponseType(typeof(Guid), 200)]
        public async Task<IActionResult> Post(UserCredentialAddRequestModel requestModel)
        {
            return Ok(await _credentialService.Add(requestModel));
        }

        [HttpPut]
        [ProducesResponseType(typeof(void), 200)]
        public async Task<IActionResult> Put(UserCredentialUpdateRequestModel requestModel)
        {
            await _credentialService.Update(requestModel);
            return Ok();
        }

        [HttpDelete]
        [ProducesResponseType(typeof(void), 200)]
        public async Task<IActionResult> Delete(Guid guid)
        {
            await _credentialService.Delete(guid);
            return Ok();
        }
    }
}