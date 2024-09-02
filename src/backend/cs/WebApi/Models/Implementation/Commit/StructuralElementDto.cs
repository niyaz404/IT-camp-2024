using WebApi.Enums;
using WebApi.Models.Abstract.Commit;

namespace WebApi.Models.Implementation.Commit;

/// <summary>
/// Dto конструктивного элемента
/// </summary>
public class StructuralElementDto : MagnetogramElementDto
{
    public StructuralElementDto() => Type = ElementType.StructuralElement;

    /// <summary>
    /// Тип конструктивного элемента
    /// </summary>
    public StructuralElementType StructuralElementType { get; set; }
}