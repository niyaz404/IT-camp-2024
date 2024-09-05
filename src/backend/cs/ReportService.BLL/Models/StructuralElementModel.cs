namespace ReportService.BLL.Models;

/// <summary>
/// Модель конструктивного элемента
/// </summary>
public class StructuralElementModel
{
    /// <summary>
    /// Идентификатор элемента
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Тип структурного элемента
    /// </summary>
    public StructuralElementType Type { get; set; }
    
    /// <summary>
    /// Х-координата начала области
    /// </summary>
    public int StartXCoordinate { get; set; }
    
    /// <summary>
    /// Х-координата конца области
    /// </summary>
    public int EndXCoordinate { get; set; }
}