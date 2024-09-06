namespace AuthService.DAL.Models;

/// <summary>
/// Сущность связи пользователя и роли
/// </summary>
public class UserToRoleEntity
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