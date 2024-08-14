using WebApi.Enums;
using WebApi.Models.Implementation.MetaData;

namespace WebApi.Models.Abstract.MetaData;

/// <summary>
/// Dto элемента на магнитограмме
/// </summary>
public abstract class MagnetogramElementDto
{
    /// <summary>
    /// Id элемента
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
    /// Координаты элемента
    /// </summary>
    public CoordinateDto Coordinate { get; set; }
    
    /// <summary>
    /// Соседний элемент справа на магнитограмме
    /// </summary>
    public MagnetogramElementDto LeftNeighbour { get; set; }
    
    /// <summary>
    /// Соседний элемент справа на магнитограмме
    /// </summary>
    public MagnetogramElementDto RightNeighbour { get; set; }
}