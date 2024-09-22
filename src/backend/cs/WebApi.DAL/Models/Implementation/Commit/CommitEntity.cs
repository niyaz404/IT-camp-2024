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

    /// <summary>
    /// Дата загрузки обработка магнитограммы
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// ФИО пользователя, запустившего обработку магнитограммы
    /// </summary>
    public string CreatedBy { get; set; }
    
    /// <summary>
    /// Список дефектов
    /// </summary>
    public List<DefectEntity> Defects { get; set; }
    
    /// <summary>
    /// Наличие дефектов в магнитограмме
    /// </summary>
    public bool IsDefective { get; set; }

    /// <summary>
    /// Список конструктивных элементов
    /// </summary>
    public List<StructuralElementEntity> StructuralElements { get; set; }
    
    /// <summary>
    /// Файл обработанной магнитограммы
    /// </summary>
    public byte[] ProcessedImage { get; set; }
}