namespace AuthService.Models;

/// <summary>
/// Модель кредов пользователя
/// </summary>
public class UserCredentials
{
    /// <summary>
    /// ФИО пользователя
    /// </summary>
    public string Username { get; set; }
    
    /// <summary>
    /// Логин пользователя
    /// </summary>
    public string Login { get; set; }
    
    /// <summary>
    /// Пароль пользователя
    /// </summary>
    public string Password { get; set; }
    
    /// <summary>
    /// Роль пользователя
    /// </summary>
    public string Role { get; set; } // Added role for registration
}