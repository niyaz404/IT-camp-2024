using DAL.Models.Implementation.Commit;

namespace DAL.Repositories.Interface;

/// <summary>
/// Интерфейс репозитория для работы с сущностью Commit
/// </summary>
public interface ICommitRepository
{
    /// <summary>
    /// Получение сущности обработки по идентификатору из БД
    /// </summary>
    public Task<CommitEntity> SelectById(Guid commitId);
    
    /// <summary>
    /// Получение всех сущностей обработок магнитограмм из БД
    /// </summary>
    /// <returns></returns>
    public Task<IEnumerable<CommitEntity>> Select();

    /// <summary>
    /// Добавление новой записи обработки в БД
    /// </summary>
    /// <returns>Идентификатор новой записи</returns>
    public Task<Guid> Insert(CommitEntity commit);

    /// <summary>
    /// Удаление записи обработки магнитограммы по идентификатору обработки
    /// </summary>
    public Task Delete(Guid commitId);
}