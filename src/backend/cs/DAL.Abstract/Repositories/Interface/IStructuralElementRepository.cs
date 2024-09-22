using DAL.Models.Implementation.Commit;

namespace DAL.Repositories.Interface;

/// <summary>
/// Интерфейс репозитория для работы с сущностью StructuralElement
/// </summary>
public interface IStructuralElementRepository
{
    /// <summary>
    /// Получение структурных элементов по идентификаторов обработки
    /// </summary>
    public Task<IEnumerable<StructuralElementEntity>> SelectByCommitId(Guid commitId);
    
    /// <summary>
    /// Добавление структурного элемента в таблицу
    /// </summary>
    public Task<Guid> Insert(StructuralElementEntity element);
}