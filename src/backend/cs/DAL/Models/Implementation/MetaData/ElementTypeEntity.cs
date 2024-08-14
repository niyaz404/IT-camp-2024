namespace DAL.Models.Implementation.MetaData;

/// <summary>
/// Сущность справочника типов элементов магнитограммы
/// </summary>
public class ElementTypeEntity
{
    // Идентификатор типа
    public int Id { get; set; }
    
    // Наименование типа
    public string Name { get; set; }
}