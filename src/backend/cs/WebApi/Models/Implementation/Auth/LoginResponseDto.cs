namespace WebApi.Models.Implementation.Auth;

/// <summary>
/// Dto ответа авторизации
/// </summary>
public class LoginResponseDto
{
    /// <summary>
    /// Токен авторизации
    /// </summary>
    public string Token { get; set; }
    
    /// <summary>
    /// Роль
    /// </summary>
    public string Role { get; set; }
}