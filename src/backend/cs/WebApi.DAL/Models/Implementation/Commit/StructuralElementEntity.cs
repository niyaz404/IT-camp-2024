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
    /// Тип элемента на магнитограмме
    /// </summary>
    public int Type { get; set; }
    
    /// <summary>
    /// Дополнительное описание элемета
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    /// Координата X на магнитограмме
    /// </summary>
    public int X { get; set; }
    
    /// <summary>
    /// Координата Y на магнитограмме
    /// </summary>
    public int Y { get; set; }
    
    /// <summary>
    /// Соседний элемент справа на магнитограмме
    /// </summary>
    public string LeftNeighbourId { get; set; }
    
    /// <summary>
    /// Тип левого соседа. 1 - дефект, 2 - конструктивный элемент
    /// </summary>
    public int LeftNeighbourType { get; set; }
    
    /// <summary>
    /// Соседний элемент справа на магнитограмме
    /// </summary>
    public string RightNeighbourId { get; set; }   
    
    /// <summary>
    /// Тип левого соседа. 1 - дефект, 2 - конструктивный элемент
    /// </summary>
    public int RightNeighbourType { get; set; }
}