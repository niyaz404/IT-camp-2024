namespace DAL.Repositories.Interface;

/// <summary>
/// Интерфейс репозитория для работы с сущностью Defect_Commit
/// </summary>
public interface IDefectToCommitRepository
{
    /// <summary>
    /// Получение списка идентификаторов дефекто по идентификатору обработки
    /// </summary>
    public Task<IEnumerable<Guid>> SelectDefectIdsByCommitIds(Guid commitId);

    /// <summary>
    /// Добавление записи в таблицу
    /// </summary>
    public Task<Guid> Insert(Guid commitId, Guid defectId);

    /// <summary>
    /// Удалить запись по идентификатору обработки
    /// </summary>
    public Task DeleteByCommitId(Guid commitId);
}