using Business.DTOs.Requests;
using Business.DTOs.Responses;
using Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace QRestoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest registerDto)
        {
            var user = new AppUser
            {
                UserName = registerDto.Username,
                FullName = registerDto.FullName,
                Email = $"{registerDto.Username}@qresto.com" // Identity üçün fake email
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync(registerDto.Role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(registerDto.Role));
                }
                await _userManager.AddToRoleAsync(user, registerDto.Role);

                return Ok("İstifadəçi və vəzifəsi uğurla yaradıldı!");
            }
            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest loginDto)
        {
            // 1. Nickname-ə görə istifadəçini tap
            var user = await _userManager.FindByNameAsync(loginDto.Username);

            // 2. Şifrəni yoxla
            if (user != null && await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                var roles = await _userManager.GetRolesAsync(user);
                var userRole = roles.FirstOrDefault();
                // 3. Hər şey OK-dirsə, vəsiqəsini (token) hazırla
                var token = await GenerateJwtToken(user);

                return Ok(new LoginResponse
                {
                    Token = token,
                    Message = "Giriş uğurludur!",
                    Role = userRole,
                    Username = user.UserName
                });
            }

            return Unauthorized("Məlumatlar səhvdir!");
        }
        private async Task<string> GenerateJwtToken(AppUser user)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);

            // İstifadəçinin rollarını (Waiter, Admin) bazadan alırıq
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName)
            };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7), // 7 gün qüvvədədir
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = jwtSettings["Issuer"],
                Audience = jwtSettings["Audience"]
            };

            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
