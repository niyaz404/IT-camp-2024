namespace WebApi.Models.Implementation.Report;

/// <summary>
/// Dto отчета
/// </summary>
public class ReportDto
{
    /// <summary>
    /// Идентификатор отчета
    /// </summary>
    public string Id { get; set; }
    
    /// <summary>
    /// Идентификатор данных о магнитограмме
    /// </summary>
    public string MetaDataId { get; set; }
    
    /// <summary>
    /// Дата создания отчета
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Файл отчета
    /// </summary>
    public IFormFile File { get; set; }
}