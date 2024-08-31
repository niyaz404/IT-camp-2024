using WebApi.DAL.Models.Abstract;

namespace WebApi.DAL.Models.Implementation.Commit;

/// <summary>
/// Сущность данных о магнитограмме
/// </summary>
public class CommitEntity : BaseEntity
{
    /// <summary>
    /// Идентификатор денных о магнитограмме
    /// </summary>
    public string Id { get; set; }
    
    /// <summary>
    /// Идентификатор магнитограммы
    /// </summary>
    public string MagnetogramId { get; set; }
    
    /// <summary>
    /// Дата обработка магнитограммы
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// ФИО пользователя, запустившего обработку магнитограммы
    /// </summary>
    public string CreatedBy { get; set; }
    
    /// <summary>
    /// Список идентификаторов дефектов
    /// </summary>
    public string[] DefectIds { get; set; }
    
    /// <summary>
    /// Список идентификаторов конструктивных элементов
    /// </summary>
    public string[] StructuralElementIds { get; set; }
    
    /// <summary>
    /// Файл исходной магнитограммы
    /// </summary>
    public byte[] OriginalMagnetogram { get; set; }
    
    /// <summary>
    /// Файл обработанной магнитограммы
    /// </summary>
    public byte[] ProcessedMagnetogram { get; set; }
}