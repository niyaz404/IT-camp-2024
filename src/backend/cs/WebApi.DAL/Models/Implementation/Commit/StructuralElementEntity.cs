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
    public string Id { get; set; }
    
    /// <summary>
    /// Тип структурного элемента
    /// </summary>
    public int StructuralElementTypeId { get; set; }
    
    /// <summary>
    /// Цвет элемента на магнитограмме
    /// </summary>
    public string Color { get; set; }
    
    /// <summary>
    /// Х-координата начала области
    /// </summary>
    public int StartXCoordinate { get; set; }
    
    /// <summary>
    /// Х-координата конца области
    /// </summary>
    public int EndXCoordinate { get; set; }
}