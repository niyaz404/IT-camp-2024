namespace WebApi.BLL.Models.Implementation.Report;

/// <summary>
/// Модель отчета
/// </summary>
public class ReportModel
{
    /// <summary>
    /// Идентификатор отчета
    /// </summary>
    public string Id { get; set; }
    
    /// <summary>
    /// Идентификатор данных о магнитограмме
    /// </summary>
    public string CommitId { get; set; }
    
    /// <summary>
    /// Дата создания отчета
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Файл отчета
    /// </summary>
    public byte[] File { get; set; }
}