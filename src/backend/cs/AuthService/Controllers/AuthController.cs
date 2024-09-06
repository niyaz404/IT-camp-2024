using AuthService.BLL.Models;
using AuthService.BLL.Services.Interface;
using AuthService.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    
    public AuthController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Регистрация пользователя
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Register([FromBody]UserModel user)
    {
        try
        {
            await _userService.Register(_mapper.Map<UserModel>(user));
        }
        catch (Exception e)
        {
            return BadRequest();
        }

        return Ok();
    }

    /// <summary>
    /// Генерация токена доступа
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> GenerateToken([FromBody] UserCredentials userCredentials)
    {
        await _userService.GenerateToken(_mapper.Map<UserCredentialsModel>(userCredentials));

        return Ok(new { Token = "tokenString" });
    }
}