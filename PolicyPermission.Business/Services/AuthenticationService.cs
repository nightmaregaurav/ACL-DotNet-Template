using Microsoft.AspNetCore.Http;
using PolicyPermission.Abstraction.Business;

namespace PolicyPermission.Business.Services
{
    internal class AuthenticationService : IAuthenticationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticationService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        
        public async Task<string> Login(string userName, string password)
        {
            // var loginId = await _userRepo.LoginAsync(_tenant, userName, password, branchId, deviceId, userAgent, ipAddress);
            //
            // if (loginId == 0)
            //     return BadRequest("Invalid User");
            //
            // var user = await _userRepo.Get(userName, _tenant);
            // var tokenHandler = new JwtSecurityTokenHandler();
            // var key = Encoding.ASCII.GetBytes(signingKey);
            //
            // var claims = new List<Claim>
            // {
            //     new(JwtRegisteredClaimNames.Jti, user.UserName.ToString(CultureInfo.CurrentCulture)),
            //     new("bid", branchId.ToString()),
            //     new("uid", user.Id.ToString()),
            //     new("cli", tenant.Id + ""),
            //     new("loc", locale),
            //     new("lgn", loginId.ToString())
            // };
            //
            // //Roles
            // // claims.Add(new Claim(ClaimTypes.Role, "User"));
            // // claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            // // claims.Add(new Claim(ClaimTypes.Role, "Banking"));
            //
            // var tokenDescriptor = new SecurityTokenDescriptor
            // {
            //     Subject = new ClaimsIdentity(claims),
            //     Expires = DateTime.UtcNow.AddHours(10),
            //     SigningCredentials =
            //         new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            // };
            //
            // var token = tokenHandler.CreateToken(tokenDescriptor);
            // var clientToken = tokenHandler.WriteToken(token);
            //
            // var identity = new ClaimsIdentity(claims, "Basic");
            // await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            //     new ClaimsPrincipal(identity), new AuthenticationProperties
            //     {
            //         ExpiresUtc = DateTime.Now.AddHours(10),
            //         IsPersistent = true
            //     }).ConfigureAwait(true);
            //
            //
            // ViewBag.Token = clientToken;
            // ViewBag.RetUrl = retUrl;
            //
            // return View();
            return "";
        }
    }
}