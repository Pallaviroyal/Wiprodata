using ChatBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IConfiguration _config;
    public AuthController(UserManager<User> um,
    SignInManager<User> sm, IConfiguration cfg)
    { _userManager = um; _signInManager = sm; _config = cfg; }
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var user = new User
        {
            UserName = dto.Email,
            Email =
        dto.Email,
            DisplayName = dto.DisplayName
        };
        var result = await _userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded) return BadRequest(result.Errors);
        return Ok(new { message = "Registered" });
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user == null) return Unauthorized();
        var ok = await _signInManager.CheckPasswordSignInAsync(user,
        dto.Password, false);
        if (!ok.Succeeded) return Unauthorized();
        return Ok(new
        {
            token = CreateToken(user),
            user = new
            {
                user.Id,
                user.Email,
                user.DisplayName
            }
        });
    }
    [Authorize]
    [HttpPost("logout")]
    public IActionResult Logout() => Ok(new
    {
        message = "Client should discard JWT"
    });
    private string CreateToken(User user)
    {
        var claims = new List<Claim>
{
new Claim(JwtRegisteredClaimNames.Sub, user.Id),
new Claim(JwtRegisteredClaimNames.Email, user.Email ?? ""),
new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
};
        var jwt = _config.GetSection("Jwt");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
        issuer: jwt["Issuer"], audience: jwt["Audience"],
        claims: claims, expires: DateTime.UtcNow.AddHours(12),
        signingCredentials: creds);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
public record RegisterDto(string Email, string Password, string DisplayName);
public record LoginDto(string Email, string Password);
