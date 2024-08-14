namespace WebApi.Models.Implementation.MetaData;

/// <summary>
/// Dto данных об обработке магнитограммы
/// </summary>
public class MetaDataDto
{
    /// <summary>
    /// Id исходной магнитограммы
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
    public List<DefectDto> Defects { get; set; }
    
    /// <summary>
    /// Список конструктивных элементов
    /// </summary>
    public List<StructuralElementDto> StructuralElements { get; set; }
    
    /// <summary>
    /// Исходная магнитограмма
    /// </summary>
    public IFormFile OriginalMagnetogram { get; set; }
    
    /// <summary>
    /// Обработанная магнитограмма
    /// </summary>
    public IFormFile ProcessedMagnetogram { get; set; }
}