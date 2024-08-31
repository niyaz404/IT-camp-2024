using WebApi.Enums;
using WebApi.Models.Implementation.Commit;

namespace WebApi.Models.Abstract.Commit;

/// <summary>
/// Dto элемента на магнитограмме
/// </summary>
public abstract class MagnetogramElementDto
{
    /// <summary>
    /// Идентификатор элемента
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Тип элемента на магнитограмме
    /// </summary>
    public ElementType Type { get; set; }
    
    /// <summary>
    /// Дополнительное описание элемета
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    /// Координата х элемента
    /// </summary>
    public int X { get; set; }
    
    /// <summary>
    /// Соседний элемент справа на магнитограмме
    /// </summary>
    public MagnetogramElementDto LeftNeighbour { get; set; }
    
    /// <summary>
    /// Соседний элемент справа на магнитограмме
    /// </summary>
    public MagnetogramElementDto RightNeighbour { get; set; }
}