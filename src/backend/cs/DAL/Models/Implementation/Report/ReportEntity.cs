using DAL.Models.Abstract;

namespace DAL.Models.Implementation.Report;

/// <summary>
/// Сущность отчета
/// </summary>
public class ReportEntity : BaseEntity
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
    public byte[] File { get; set; }
}