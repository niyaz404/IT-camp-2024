using DAL.Models.Implementation.Commit;

namespace DAL.Repositories.Interface;

/// <summary>
/// Интерфейс репозитория для работы с сущностью Commit
/// </summary>
public interface ICommitRepository
{
    /// <summary>
    /// Получение сущности обработки по идентификатору
    /// </summary>
    public Task<CommitEntity> SelectById(Guid commitId);
}