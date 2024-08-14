using BLL.Enums;
using BLL.Models.Abstract.MetaData;

namespace BLL.Models.Implementation.MetaData;

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