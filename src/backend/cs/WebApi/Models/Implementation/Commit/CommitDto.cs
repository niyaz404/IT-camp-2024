namespace WebApi.Models.Implementation.Commit;

/// <summary>
/// Dto данных об обработке магнитограммы
/// </summary>
public class CommitDto
{
    /// <summary>
    /// Идентификатор обработки
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Идентификатор исходной магнитограммы
    /// </summary>
    public Guid MagnetogramId { get; set; }
    
    /// <summary>
    /// Наименование фиксации
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Дата создание обработки (метаданных магнитограммы)
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// ФИО автора коммита
    /// </summary>
    public string CreatedBy { get; set; }
    
    /// <summary>
    /// Наличие дефектов в магнитограмме
    /// </summary>
    public bool IsDefective { get; set; }
    
    /// <summary>
    /// Список дефектов
    /// </summary>
    public List<DefectDto> Defects { get; set; }
    
    /// <summary>
    /// Список конструктивных элементов
    /// </summary>
    public List<StructuralElementDto> StructuralElements { get; set; }
    
    /// <summary>
    /// Обработанная магнитограмма
    /// </summary>
    public string ProcessedImage { get; set; }
}