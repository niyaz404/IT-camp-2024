using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AuthService.BLL.Helpers;
using AuthService.BLL.Models;
using AuthService.BLL.Services.Interface;
using AuthService.DAL.Models;
using AuthService.DAL.Repositories.Interface;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.BLL.Services.Implementation;

/// <summary>
/// Сервис аутентификации на уровне бизнес-логики
/// </summary>
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    public async Task<string> GenerateToken(UserCredentialsModel userCredentials)
    {
        var user = await _userRepository.SelectByLogin(userCredentials.Login);
        
        if (user == null)
        {
            throw new Exception("User not found");
        }
        
        var hashedPassword = PasswordHelper.HashPassword(userCredentials.Password, user.PasswordSalt);
        
        if (user.PasswordHash != hashedPassword)
        {
            throw new Exception("Password is incorrect");
        }
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = "your_secret_key_here"u8.ToArray();
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = "AuthService",
            Audience = "WebAPI",
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public async Task Register(UserModel user)
    {
        if(_userRepository.SelectByLogin(user.Login) != null)
            throw new Exception("User already exist");

        var salt = PasswordHelper.GenerateSalt();
        var hashedPassword = PasswordHelper.HashPassword(user.Password, salt);

        var userEntity = new UserEntity();

        await _userRepository.Insert(userEntity);
    }
}