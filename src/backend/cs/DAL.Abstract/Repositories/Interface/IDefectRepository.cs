using DAL.Models.Implementation.Commit;

namespace DAL.Repositories.Interface;

/// <summary>
/// Интерфейс репозитория для работы с сущностью Defect
/// </summary>
public interface IDefectRepository
{
    /// <summary>
    /// Получение дефектов по идентификатору обработки
    /// </summary>
    public Task<IEnumerable<DefectEntity>> SelectByCommitId(Guid commitId);
}