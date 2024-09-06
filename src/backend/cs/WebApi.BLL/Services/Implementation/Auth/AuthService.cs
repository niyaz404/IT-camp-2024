using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using WebApi.BLL.Models.Implementation.Auth;
using WebApi.BLL.Services.Interface.Auth;
using WebApi.DAL.Providers.Interface;

namespace WebApi.BLL.Services.Implementation.Auth;

/// <summary>
/// Сервис для работы с авторизацией
/// </summary>
public class AuthService : IAuthService
{
    private readonly IAuthProvider _authProvider;
    private readonly IMapper _mapper;
    
    public AuthService(IAuthProvider authProvider, IMapper mapper)
    {
        _authProvider = authProvider;
        _mapper = mapper;
    }
    
    public async Task<LoginResponseModel> Login(UserCredentialsModel userCredentials)
    {
        var token = await _authProvider.Login(_mapper.Map<UserCredentials>(userCredentials));
        // Проверка и обработка JWT-токена
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
        var role = jsonToken?.Claims.First(claim => claim.Type == "role").Value;

        return new LoginResponseModel { Token = token, Role = role };
    }
}