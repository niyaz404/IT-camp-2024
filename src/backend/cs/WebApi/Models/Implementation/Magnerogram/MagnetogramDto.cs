namespace WebApi.Models.Implementation.Magnerogram;

/// <summary>
/// Параметры запроса с данными магнитограммы
/// </summary>
public class MagnetogramDto
{
    /// <summary>
    /// Название магнитограммы
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// ФИО загрузившего
    /// </summary>
    public string CreatedBy { get; set; }
    
    /// <summary>
    /// Время сохранения магнитограммы
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Файл магнитограммы в формате .pkl
    /// </summary>
    public IFormFile File { get; set; }
}