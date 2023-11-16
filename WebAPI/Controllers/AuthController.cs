using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Shared.DTOs;
using Shared.Models;
using WebAPI.Services;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly IAuthService _authService;
    private readonly AppDbContext _context;


    public AuthController(IConfiguration config, IAuthService authService, AppDbContext context)
    {
        _config = config;
        _authService = authService;
        _context = context;
    }

    [HttpPost, Route("register")]
    public async Task<ActionResult> Register([FromBody] UserModel? userModel)
    {
        await _authService.RegisterUser(userModel);
        return Ok();
    }

    [HttpPost, Route("login")]
    public async Task<ActionResult> Login([FromBody] UserLoginDto userLoginDto)
    {
        try
        {
            UserModel userModel = await _authService.ValidateUser(userLoginDto.Username, userLoginDto.Password);
            string token = GenerateJwt(userModel);
            userModel.Token = token;
            _context.SaveChanges();
            return Ok(token);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    private string GenerateJwt(UserModel user)
    {
        List<Claim> claims = GenerateClaims(user);

        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        SigningCredentials signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        JwtHeader header = new JwtHeader(signIn);

        JwtPayload payload = new JwtPayload(
            _config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            null,
            DateTime.UtcNow.AddMinutes(60));

        JwtSecurityToken token = new JwtSecurityToken(header, payload);

        string serializedToken = new JwtSecurityTokenHandler().WriteToken(token);
        return serializedToken;
    }

    private List<Claim> GenerateClaims(UserModel user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, _config["Jwt:Subject"]),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim("SecurityLevel", user.SecurityLevel.ToString())
        };
        return claims.ToList();
    }
}