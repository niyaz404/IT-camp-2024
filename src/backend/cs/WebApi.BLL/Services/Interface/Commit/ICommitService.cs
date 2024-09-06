using WebApi.BLL.Models.Implementation.Commit;

namespace WebApi.BLL.Services.Interface.Commit;

/// <summary>
/// Интерфейс сервиса для работы с Обработкой
/// </summary>
public interface ICommitService
{
    /// <summary>
    /// Сохранения обработки
    /// </summary>
    public Task Save(CommitModel commit);

    /// <summary>
    /// Получение обработки по идентификатору
    /// </summary>
    public Task<CommitModel> Get(Guid commitId);

    /// <summary>
    /// Получение списка всех обработок магнитограмм
    /// </summary>
    public Task<IEnumerable<CommitModel>> GetAll();

    /// <summary>
    /// Удаление обработки магнитограммы по идентификатору обработки
    /// </summary>
    public Task Delete(Guid commitId);
}