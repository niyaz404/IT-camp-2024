namespace ReportService.Models;

/// <summary>
/// Dto обработки магнитограммы
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
    public bool IsDefective => Defects?.Any() ?? false;

    /// <summary>
    /// Список дефектов
    /// </summary>
    public List<DefectDto> Defects { get; set; } = new ();

    /// <summary>
    /// Список конструктивных элементов
    /// </summary>
    public List<StructuralElementDto> StructuralElements { get; set; } = new ();
    
    /// <summary>
    /// Обработанная магнитограмма
    /// </summary>
    public string ProcessedImage { get; set; }
}