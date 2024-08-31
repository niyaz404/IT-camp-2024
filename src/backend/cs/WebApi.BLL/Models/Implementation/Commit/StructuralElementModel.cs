using WebApi.BLL.Enums;
using WebApi.BLL.Models.Abstract.Commit;

namespace WebApi.BLL.Models.Implementation.Commit;

/// <summary>
/// Модель конструктивного элемента
/// </summary>
public class StructuralElementModel : MagnetogramElementModel
{
    /// <summary>
    /// Тип конструктивного элемента
    /// </summary>
    public StructuralElementType StructuralElementTypeType { get; set; }
}