namespace WebApi.BLL.Models.Implementation.Auth;

/// <summary>
/// Модель данных пользователя
/// </summary>
public class UserCredentials
{
    /// <summary>
    /// Логин пользователя
    /// </summary>
    public string Login { get; set; }
    
    /// <summary>
    /// Пароль пользователя
    /// </summary>
    public string Password { get; set; }
}