using WebApi.Enums;

namespace WebApi.Models.Abstract.Commit;

/// <summary>
/// Dto элемента на магнитограмме
/// </summary>
public abstract class MagnetogramElementDto
{
    /// <summary>
    /// Идентификатор элемента
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Тип элемента на магнитограмме
    /// </summary>
    public ElementType Type { get; protected set; }
    
    /// <summary>
    /// Х-координата начала области
    /// </summary>
    public int StartXCoordinate { get; set; }
    
    /// <summary>
    /// Х-координата конца области
    /// </summary>
    public int EndXCoordinate { get; set; }
}