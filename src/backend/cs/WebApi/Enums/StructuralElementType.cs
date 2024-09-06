namespace WebApi.Enums;

/// <summary>
/// Типы конструктивных элементов
/// </summary>
public enum StructuralElementType
{
    /// <summary>
    /// Отсутствует
    /// </summary>
    None = 0,
    
    /// <summary>
    /// Сварной шов
    /// </summary>
    WeldSeam = 1,
    
    /// <summary>
    /// Изгиб
    /// </summary>
    Bend = 2,
    
    /// <summary>
    /// Разветвление
    /// </summary>
    Branching = 3,
    
    /// <summary>
    /// Заплатка
    /// </summary>
    Patch = 4
}