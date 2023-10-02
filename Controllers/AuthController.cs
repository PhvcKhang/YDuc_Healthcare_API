using AutoMapper;
using HealthCareApplication.Domains.Models;
using HealthCareApplication.Identity.Authentication;
using HealthCareApplication.Identity.Helpers;
using HealthCareApplication.Identity.Models;
using HealthCareApplication.Identity.Resources;
using HealthCareApplication.Resource.Persons;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security.Claims;

namespace HealthCareApplication.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly UserManager<Person> _userManager;
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly IMapper _mapper;

        public AuthController(UserManager<Person> userManager, IJwtFactory jwtFactory, IOptions<JwtIssuerOptions> jwtOptions, IMapper mapper)
        {
            _userManager = userManager;
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("SignIn")]
        public async Task<IActionResult> SignIn([FromBody] Credential credentials)
        {
            try
            {
                var identity = await GetClaimsIdentity(credentials.Username, credentials.Password);
                if (identity is null)
                {
                    throw new ArgumentException("Username or password is invalid");
                }
                var user = await _userManager.FindByNameAsync(credentials.Username);
                var roles = await _userManager.GetRolesAsync(user);
                var jwt = await Tokens.GenerateJwt(identity, _jwtFactory, credentials.Username, roles, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });

                var userViewModel = _mapper.Map<UserViewModel>(user);
                var loginResource = new LoginResource()
                {
                    Token = jwt,
                    User = userViewModel,
                    Roles = roles.ToList(),
                };

                return new OkObjectResult(loginResource);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }
        private async Task<ClaimsIdentity> GetClaimsIdentity(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return await Task.FromResult<ClaimsIdentity>(null);
            }
            var userToVerify = await _userManager.FindByNameAsync(username);
            if (userToVerify is null)
            {
                return await Task.FromResult<ClaimsIdentity>(null);
            }
            // check the credentials
            if (await _userManager.CheckPasswordAsync(userToVerify, password))
            {
                return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(username, userToVerify.Id));
            }
            // Credentials are invalid, or account doesn't exist
            return await Task.FromResult<ClaimsIdentity>(null);
        }
    }
}
