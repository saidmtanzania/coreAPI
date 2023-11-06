using coreAPI.Models.DTO.Auth;
using coreAPI.Repositories.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace coreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepository _tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            _userManager = userManager;
            _tokenRepository = tokenRepository;
        }

        //POST: api/Auth/Register
        [HttpPost()]
        [Route("Register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequest)
        {
            // Handle the case where no roles are provided
            if (registerRequest.Roles == null || !registerRequest.Roles.Any() || registerRequest.Roles.Any(string.IsNullOrWhiteSpace))
            {
                return BadRequest("User registered Failed. No roles provided.");
            }

            var identityUser = new IdentityUser
            {
                UserName = registerRequest.Username,
                Email = registerRequest.Username,
            };

            var identityResult = await _userManager.CreateAsync(identityUser, registerRequest.Password);

            // Handle the case where user creation fails
            if (!identityResult.Succeeded)
            {
                return BadRequest(identityResult.Errors);
                // return BadRequest("User registered Failed. User creation failed.");
            }

            identityResult = await _userManager.AddToRolesAsync(identityUser, registerRequest.Roles);

            if (identityResult.Succeeded)
            {
                return Ok("User registered Succeeded");
            }

            // Handle the case where adding roles fails
            return BadRequest("User registered Failed. Adding roles failed.");

        }

        [HttpPost()]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
        {
            var user = await _userManager.FindByEmailAsync(loginRequest.Username);
            if (user != null)
            {
                var checkPass = await _userManager.CheckPasswordAsync(user, loginRequest.Password);
                if (checkPass)
                {
                    var role = await _userManager.GetRolesAsync(user);
                    if (role != null)
                    {
                        var jwtToken = _tokenRepository.CreateJWTToken(user, role.ToList());
                        //Create Token
                        var response = new LoginResponseDto(jwtToken);
                        return Ok(response);
                    }
                }
            }

            return BadRequest("Username or Password is incorrect");
        }
    }
}