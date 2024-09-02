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
    public ElementType Type { get; protected set; }
    
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