using DAL.Models.Implementation.Commit;

namespace DAL.Repositories.Interface;

/// <summary>
/// Интерфейс репозитория для работы с сущностью StructuralElement_Commit
/// </summary>
public interface IStructuralElementToCommitRepository
{
    /// <summary>
    /// Получение списка идентификаторов структурных элементов по идентификатору обработки
    /// </summary>
    public Task<IEnumerable<Guid>> SelectStructuralElementIdsByCommitIds(Guid commitId);
}