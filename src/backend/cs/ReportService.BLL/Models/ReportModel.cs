namespace ReportService.BLL.Models;

/// <summary>
/// Модель отчета
/// </summary>
public class ReportModel
{
    /// <summary>
    /// Идентификатор отчета
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Идентификатор данных о магнитограмме
    /// </summary>
    public Guid CommitId { get; set; }
    
    /// <summary>
    /// Дата создания отчета
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Файл отчета
    /// </summary>
    public byte[] File { get; set; }
}