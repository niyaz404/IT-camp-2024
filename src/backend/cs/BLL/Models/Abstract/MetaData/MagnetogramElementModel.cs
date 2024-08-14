using BLL.Enums;
using BLL.Models.Implementation.MetaData;

namespace BLL.Models.Abstract.MetaData;

/// <summary>
/// Элемент на магнитограмме
/// </summary>
public abstract class MagnetogramElementModel
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
    public CoordinateModel CoordinateModel { get; set; }
    
    /// <summary>
    /// Соседний элемент справа на магнитограмме
    /// </summary>
    public MagnetogramElementModel LeftNeighbour { get; set; }
    
    /// <summary>
    /// Соседний элемент справа на магнитограмме
    /// </summary>
    public MagnetogramElementModel RightNeighbourId { get; set; }
}