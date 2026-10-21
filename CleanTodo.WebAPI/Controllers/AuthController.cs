using CleanTodo.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[ApiController]
[Route("api/[controller]")]

public class AuthController(AppDbContext db, IConfiguration configuration) :ControllerBase
{
    private readonly AppDbContext _db = db;
    private readonly IConfiguration _configuration = configuration;
    private readonly PasswordHasher<User> _passwordHasher = new();

    [HttpPost("register")]
    public async Task<IActionResult> Register(
    [FromBody] RegisterModel register)
    {
        var username = register.Username.Trim();

        if (username.Length < 3 || username.Length > 100)
        {
            return BadRequest("Username must be between 3 and 100 characters.");
        }

        if (register.Password.Length < 12)
        {
            return BadRequest("Password must be at least 12 characters.");
        }

        var existingUser = await _db.Users
          .AnyAsync(u => u.Username == username);

        if (existingUser)
        {
            return Conflict("Username is already taken.");
        }

        var user = new User
        {
            Username = username,
            Role = "User"
        };

        user.PasswordHash = _passwordHasher.HashPassword(
          user,
          register.Password
        );

        _db.Users.Add(user);
        await _db.SaveChangesAsync();

        return StatusCode(201, new
        {
            message = "Account created successfully.",
            userId = user.Id,
            username = user.Username
        });
    }


    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel login)
    {

        var username = login.Username.Trim();

        var user = await _db.Users.SingleOrDefaultAsync(u => u.Username == username);

        if (user == null)
        {
            return Unauthorized("Invalid username or password.");
        }

        var passwordResult = _passwordHasher.VerifyHashedPassword(
          user,
          user.PasswordHash,
          login.Password
        );

        if (passwordResult == PasswordVerificationResult.Failed)
        {
            return Unauthorized("Invalid username or password.");
        }

        if (passwordResult == PasswordVerificationResult.SuccessRehashNeeded)
        {
            user.PasswordHash = _passwordHasher.HashPassword(
              user,
              login.Password
            );

            await _db.SaveChangesAsync();
        }

        var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
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
public class RegisterModel
{
    public string Username { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}

public class LoginModel
{
    public string Username { get; set; }
    public string Password { get; set; }
}