using DAL.Models.Abstract;

namespace DAL.Models.Implementation.Commit;

/// <summary>
/// Сущность данных о магнитограмме
/// </summary>
public class CommitEntity : BaseEntity
{
    /// <summary>
    /// Идентификатор денных о магнитограмме
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Идентификатор магнитограммы
    /// </summary>
    public Guid MagnetogramId { get; set; }
    
    /// <summary>
    /// Наименование фиксации
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Дата обработка магнитограммы
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// ФИО пользователя, запустившего обработку магнитограммы
    /// </summary>
    public string CreatedBy { get; set; }
    
    /// <summary>
    /// Файл обработанной магнитограммы
    /// </summary>
    public byte[] ProcessedImage { get; set; }
}