using AnsarTask.Core.Constants;
using AnsarTask.Core.DTOs;
using AnsarTask.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
namespace AnsarTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ITokenService _tokenService;
        public AuthController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) return Unauthorized();
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded) return Unauthorized();
            UserDto userDto = new()
            {
                Email = loginDto.Email,
                Token = _tokenService.CreateToken(user, await GetRole(user))
            };
            userDto.Token = _tokenService.CreateToken(user, await GetRole(user));
            return userDto;
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var user = await _userManager.FindByEmailAsync(registerDto.Email);
            if (user != null) return BadRequest("this mail already exists");
            user = new();
            user.Email = registerDto.Email;
            user.UserName = registerDto.Email.ToLower();
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);
            registerDto.Role = string.Equals(registerDto.Role.Trim(), AppRoles.Admin, StringComparison.OrdinalIgnoreCase) ? AppRoles.User : AppRoles.Admin;
            await _userManager.AddToRoleAsync(user, registerDto.Role!);
            UserDto userDto = new()
            {
                Email = registerDto.Email,
                Token = _tokenService.CreateToken(user, await GetRole(user))
            };
            return userDto;
        }

        private async Task<string> GetRole(IdentityUser user)
        {
            var x = await _userManager.IsInRoleAsync(user, AppRoles.Admin);
            return (await _userManager.IsInRoleAsync(user, AppRoles.Admin)) ? AppRoles.Admin : AppRoles.User;
        }
    }
}
