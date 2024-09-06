namespace AuthService.BLL.Models;

/// <summary>
/// Модель связи пользователя и роли
/// </summary>
public class UserToRoleModel
{
    /// <summary>
    /// Идентификатор связи
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Идентификатор роли
    /// </summary>
    public int RoleId { get; set; }
}