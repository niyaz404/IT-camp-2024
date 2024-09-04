namespace DAL.Models.Implementation.Commit;

/// <summary>
/// Сущность справочника типов конструктивных элементов
/// </summary>
public class StructuralElementTypeEntity
{
    // Идентификатор типа
    public int Id { get; set; }
    
    // Наименование типа
    public string Name { get; set; }
}