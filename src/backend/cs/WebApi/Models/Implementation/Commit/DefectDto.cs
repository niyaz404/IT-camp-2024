using WebApi.Enums;
using WebApi.Models.Abstract.Commit;

namespace WebApi.Models.Implementation.Commit;

/// <summary>
/// Dto дефекта
/// </summary>
public class DefectDto : MagnetogramElementDto
{
    public DefectDto() => Type = ElementType.Defect;
    
    /// <summary>
    /// Дополнительное описание элемета
    /// </summary>
    public string Description { get; set; }
}