using WebApi.Enums;
using WebApi.Models.Abstract.MetaData;

namespace WebApi.Models.Implementation.MetaData;

/// <summary>
/// Dto конструктивного элемента
/// </summary>
public class StructuralElementDto : MagnetogramElementDto
{
    /// <summary>
    /// Тип конструктивного элемента
    /// </summary>
    public StructuralElementType StructuralElementType { get; set; }
}