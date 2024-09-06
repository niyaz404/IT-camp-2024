namespace ReportService.Models;

/// <summary>
/// Dto структурного элемента
/// </summary>
public class StructuralElementDto
{
    /// <summary>
    /// Идентификатор элемента
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Тип структурного элемента
    /// </summary>
    public StructuralElementTypeDto Type { get; set; }
    
    /// <summary>
    /// Х-координата начала области
    /// </summary>
    public int StartXCoordinate { get; set; }
    
    /// <summary>
    /// Х-координата конца области
    /// </summary>
    public int EndXCoordinate { get; set; }
}