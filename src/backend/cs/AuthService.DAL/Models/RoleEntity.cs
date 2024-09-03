namespace AuthService.DAL.Models;

/// <summary>
/// Сущность роли
/// </summary>
public class RoleEntity
{
    /// <summary>
    /// Идентификатор роли
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Наименование роли
    /// </summary>
    public string Name { get; set; }
}