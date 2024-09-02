using WebApi.DAL.Models.Abstract;

namespace WebApi.DAL.Models.Implementation.Commit;

/// <summary>
/// Сущность дефекта
/// </summary>
public class DefectEntity : BaseEntity
{
    /// <summary>
    /// Идентификатор элемента
    /// </summary>
    public string Id { get; set; }
    
    /// <summary>
    /// Дополнительное описание элемета
    /// </summary>
    public string Description { get; set; }
    
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
    
    /// <summary>
    /// Идентификатор ближайшего структурного элемента слева
    /// </summary>
    public string LeftStructuralElementId { get; set; }
    
    /// <summary>
    /// Идентификатор ближайшего структурного элемента справа
    /// </summary>
    public string RightStructuralElementId { get; set; }
}