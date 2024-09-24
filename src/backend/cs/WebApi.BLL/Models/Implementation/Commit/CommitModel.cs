namespace WebApi.BLL.Models.Implementation.Commit;

/// <summary>
/// Модель метоаданных
/// </summary>
public class CommitModel
{
    /// <summary>
    /// Идентификатор исходной магнитограммы
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
    public List<DefectModel> Defects { get; set; }
    
    /// <summary>
    /// Список конструктивных элементов
    /// </summary>
    public List<StructuralElementModel> StructuralElements { get; set; }
    
    /// <summary>
    /// Обработанная магнитограмма
    /// </summary>
    public byte[] ProcessedImage { get; set; }
    
    /// <summary>
    /// Файл исходной магнитограммы
    /// </summary>
    public byte[] OriginalImage { get; set; }
}