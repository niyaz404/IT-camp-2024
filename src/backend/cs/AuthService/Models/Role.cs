namespace AuthService.Models;

/// <summary>
/// Модель роли
/// </summary>
public class Role
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