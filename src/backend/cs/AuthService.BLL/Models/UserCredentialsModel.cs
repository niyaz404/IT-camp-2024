namespace AuthService.BLL.Models;

/// <summary>
/// Модель кредов пользователя
/// </summary>
public class UserCredentialsModel
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