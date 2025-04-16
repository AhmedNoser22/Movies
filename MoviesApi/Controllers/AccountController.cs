using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MoviesApi.DTOS;
using MoviesApi.Models;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public AccountController(UserManager<AppUser> UserManager,IConfiguration configuration)
        {
            _UserManager = UserManager;
            Configuration = configuration;
        }
        public UserManager<AppUser> _UserManager { get; set; }
        public IConfiguration Configuration { get; }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(DtoNewUser newUser)
        {
            if (ModelState.IsValid)
            {
                AppUser _AppUser = new()
                {
                    UserName = newUser.UserName,
                    Email = newUser.Email
                };
                IdentityResult result = await _UserManager.CreateAsync(_AppUser, newUser.Password);
                if (result.Succeeded)
                {
                    return Ok("Success");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("",item.Description);
                    }
                }
            }
            return BadRequest(ModelState);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(DTOLogin login)
        {
            if (ModelState.IsValid)
            {
                var user = await _UserManager.FindByNameAsync(login.userName);
                if (user != null)
                {
                    if (await _UserManager.CheckPasswordAsync(user, login.password))
                    {
                        List<Claim> claims = new List<Claim>();
                        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                        claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                        var roles = await _UserManager.GetRolesAsync(user);
                        foreach (var item in roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, item));
                        }
                        var key = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(Configuration["JWT:SercritKey"]));
                        var sign = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var Token = new JwtSecurityToken
                            (
                            issuer: Configuration["JWT:Issuer"],
                            audience: Configuration["JWT:Audience"],
                            claims: claims,
                            signingCredentials: sign,
                            expires: DateTime.Now.AddHours(5)
                            );
                        var _token = new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(Token),
                            expiration = Token.ValidTo
                        };
                        return Ok(_token);
                          


                    }
                }
                return BadRequest("Name or Password InValid");
            }
            return BadRequest(ModelState);
        }

    }
}
