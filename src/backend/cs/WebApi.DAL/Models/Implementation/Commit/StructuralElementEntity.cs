using WebApi.DAL.Models.Abstract;

namespace WebApi.DAL.Models.Implementation.Commit;

/// <summary>
/// Сущность конструктивного элемента
/// </summary>
public class StructuralElementEntity : BaseEntity
{
    /// <summary>
    /// Идентификатор элемента
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Тип структурного элемента
    /// </summary>
    public StructuralElementTypeEntity Type { get; set; }
    
    /// <summary>
    /// Х-координата начала области
    /// </summary>
    public int StartXCoordinate { get; set; }
    
    /// <summary>
    /// Х-координата конца области
    /// </summary>
    public int EndXCoordinate { get; set; }
}