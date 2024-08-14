namespace BLL.Models.Implementation.Magnetogram;

/// <summary>
/// Модель магнитограммы
/// </summary>
public class MagnetogramModel
{
    /// <summary>
    /// Название магнитограммы
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Название объекта магнитограммы
    /// </summary>
    public string ObjectName { get; set; }
    
    /// <summary>
    /// Время сохранения магнитограммы
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Файл магнитограммы в формате .pkl
    /// </summary>
    public byte[] File { get; set; }
}