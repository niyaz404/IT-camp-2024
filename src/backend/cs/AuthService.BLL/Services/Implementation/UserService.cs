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
    private readonly IUserToRoleRepository _userToRoleRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IUserToRoleRepository userToRoleRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _userToRoleRepository = userToRoleRepository;
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
        if(await _userRepository.SelectByLogin(user.Login) != null)
            throw new Exception("User already exist");

        var userEntity = _mapper.Map<UserEntity>(user);

        await _userRepository.Insert(userEntity);

        await _userToRoleRepository.Insert(new UserToRoleEntity
            { UserId = await _userRepository.SelectIdByLogin(user.Login), RoleId = user.RoleId });
    }
}