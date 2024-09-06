using System.ComponentModel.DataAnnotations.Schema;
using WebApi.DAL.Models.Abstract;

namespace WebApi.DAL.Models.Implementation.Magnetogram;

/// <summary>
/// Сущность магнитограммы
/// </summary>
public class MagnetogramEntity : BaseEntity
{
    /// <summary>
    /// Идентификатор магнитограммы
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// ФИО загрузившего магнитограмму
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// ФИО загрузившего магнитограмму
    /// </summary>
    public string Name { get; set; } = "dfg";

    private DateTime _createdAt;

    /// <summary>
    /// Дата загрузки файла магнитограммы
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Файл магнитограммы в двоичном формате
    /// </summary>
    public string File { get; set; }
}