using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthService.Helpers;
using AuthService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{

    [HttpPost("register")]
    public IActionResult Register([FromBody] UserCredentials userCredentials)
    {
        // if (_context.Users.Any(u => u.Username == userCredentials.Login))
        // {
        //     return BadRequest("User already exists.");
        // }
        //
        // var salt = PasswordHelper.GenerateSalt();
        // var hashedPassword = PasswordHelper.HashPassword(userCredentials.Password, salt);
        //
        // var newUser = new User
        // {
        //     Username = userCredentials.Username,
        //     Login = userCredentials.Login,
        //     PasswordHash = hashedPassword,
        //     PasswordSalt = salt,
        //     Role = userCredentials.Role
        // };
        //
        // _context.Users.Add(newUser);
        // _context.SaveChanges();

        return Ok("User registered successfully.");
    }

    [HttpPost("token")]
    public IActionResult GenerateToken([FromBody] UserCredentials userCredentials)
    {
        // var user = _context.Users.SingleOrDefault(u => u.Username == userCredentials.Username);
        //
        // if (user == null)
        // {
        //     return Unauthorized("Invalid username or password.");
        // }
        //
        // // Hash the provided password with the stored salt
        // var hashedPassword = PasswordHelper.HashPassword(userCredentials.Password, user.PasswordSalt);
        //
        // // Verify the hashed password matches the stored hash
        // if (user.PasswordHash != hashedPassword)
        // {
        //     return Unauthorized("Invalid username or password.");
        // }
        //
        // var tokenHandler = new JwtSecurityTokenHandler();
        // var key = "your_secret_key_here"u8.ToArray();
        //
        // var tokenDescriptor = new SecurityTokenDescriptor
        // {
        //     Subject = new ClaimsIdentity(new[]
        //     {
        //         new Claim(ClaimTypes.Name, user.Username),
        //         new Claim(ClaimTypes.Role, user.Role)
        //     }),
        //     Expires = DateTime.UtcNow.AddHours(1),
        //     Issuer = "AuthService",
        //     Audience = "WebAPI",
        //     SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        // };
        //
        // var token = tokenHandler.CreateToken(tokenDescriptor);
        // var tokenString = tokenHandler.WriteToken(token);

        return Ok(new { Token = "tokenString" });
    }
}