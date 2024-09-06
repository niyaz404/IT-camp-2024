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
}