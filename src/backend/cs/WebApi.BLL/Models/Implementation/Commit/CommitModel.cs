namespace WebApi.BLL.Models.Implementation.Commit;

/// <summary>
/// Модель метоаданных
/// </summary>
public class CommitModel
{
    /// <summary>
    /// Идентификатор исходной магнитограммы
    /// </summary>
    public string MagnetogramId { get; set; }
    
    /// <summary>
    /// Дата создание обработки (метаданных магнитограммы)
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
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
    /// Исходная магнитограмма
    /// </summary>
    public byte[] OriginalMagnetogram { get; set; }
    
    /// <summary>
    /// Обработанная магнитограмма
    /// </summary>
    public byte[] ProcessedMagnetogram { get; set; }
}