using System.ComponentModel.DataAnnotations.Schema;
using DAL.Models.Abstract;

namespace DAL.Models.Implementation.Magnetogram;

/// <summary>
/// Сущность магнитограммы
/// </summary>
public class MagnetogramEntity : BaseEntity
{
    /// <summary>
    /// Идентификатор магнитограммы
    /// </summary>
    [Column("id")]
    public Guid Id { get; set; }
    
    /// <summary>
    /// ФИО загрузившего магнитограмму
    /// </summary>
    [Column("createdby")]
    public string CreatedBy { get; set; }
    
    /// <summary>
    /// Дата загрузки файла магнитограммы
    /// </summary>
    [Column("createdat")]
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Файл магнитограммы в двоичном формате
    /// </summary>
    [Column("file")]
    public byte[] File { get; set; }
}