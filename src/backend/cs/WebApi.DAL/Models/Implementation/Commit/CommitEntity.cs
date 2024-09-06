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
    public Guid Id { get; set; }
    
    /// <summary>
    /// Идентификатор магнитограммы
    /// </summary>
    public Guid MagnetogramId { get; set; }
    
    /// <summary>
    /// Наименование фиксации
    /// </summary>
    public string Name { get; set; }
    
    private DateTime _createdAt;

    /// <summary>
    /// Дата загрузки обработка магнитограммы
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// ФИО пользователя, запустившего обработку магнитограммы
    /// </summary>
    public string CreatedBy { get; set; }
    
    /// <summary>
    /// Список идентификаторов дефектов
    /// </summary>
    public Guid[] DefectIds { get; set; }
    
    /// <summary>
    /// Список идентификаторов конструктивных элементов
    /// </summary>
    public Guid[] StructuralElementIds { get; set; }
    
    /// <summary>
    /// Файл обработанной магнитограммы
    /// </summary>
    public byte[] ProcessedImage { get; set; }
}