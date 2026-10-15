using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using System.Text;

[ApiController]
[Route("api/[controller]")]

public class AuthController(IConfiguration configuration) :ControllerBase
{
    private readonly IConfiguration _configuration = configuration;

    
    [HttpPost("register")]
//write some shit here idk man


    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginModel login)
    {

        if (login.Username == "admin" && login.Password == "password") // Replace with real validation
        {

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, login.Username),
                new Claim(ClaimTypes.Role, "User")
            };

            var jwtKey = _configuration["Jwt:Key"]
              ?? throw new InvalidOperationException("JWT signing key is missing");

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey)
                );
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "your-app",
                audience: "your-app",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }

        return Unauthorized();
    }
    [Authorize(Roles = "User")]
    [HttpGet("dashboard")]
    public IActionResult GetDashboard()
    {
        return Ok("Welcome to the user dashboard!");
    }
    [Authorize(Roles = "Admin")]
    [HttpGet("admindashboard")]
    public IActionResult GetAdminDashboard()
    {
        return Ok("Welcome to the admin dashboard!");
    }
}

public class LoginModel
{
    public string Username { get; set; }
    public string Password { get; set; }
}