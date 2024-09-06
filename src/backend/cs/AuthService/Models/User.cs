namespace AuthService.Models;

/// <summary>
/// Модель пользователя
/// </summary>
public class User
{
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Фио пользователя
    /// </summary>
    public string Username { get; set; }
    
    /// <summary>
    /// Логин пользователя
    /// </summary>
    public string Login { get; set; }
    
    /// <summary>
    /// Хеш пароля
    /// </summary>
    public string PasswordHash { get; set; }
    
    /// <summary>
    /// Соль пароля
    /// </summary>
    public string PasswordSalt { get; set; }
    
    /// <summary>
    /// Роль пользователя
    /// </summary>
    public string Role { get; set; } 
}