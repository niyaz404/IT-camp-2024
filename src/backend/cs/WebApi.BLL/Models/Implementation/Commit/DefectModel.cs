using WebApi.BLL.Enums;
using WebApi.BLL.Models.Abstract.Commit;

namespace WebApi.BLL.Models.Implementation.Commit;

/// <summary>
///  Модель дефекта
/// </summary>
public class DefectModel : MagnetogramElementModel
{
    public DefectModel() => Type = ElementType.Defect;
    
    /// <summary>
    /// Дополнительное описание элемета
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    /// Идентификатор ближайшего структурного элемента слева
    /// </summary>
    public string LeftStructuralElementId { get; set; }
    
    /// <summary>
    /// Идентификатор ближайшего структурного элемента справа
    /// </summary>
    public string RightStructuralElementId { get; set; }
}