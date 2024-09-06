using ReportService.BLL.Models;

namespace ReportService.BLL.Services.Interface;

/// <summary>
/// Интерфейс сервиса для работы с обработками магнитограмм
/// </summary>
public interface ICommitService
{
    /// <summary>
    /// Сохранение магнитограммы
    /// </summary>
    /// <returns>Идентификатор сохраненной обработки магнитограммы</returns>
    public Task<Guid> Save(CommitModel commit);

    /// <summary>
    /// Получение обработки по идентификатору
    /// </summary>
    public Task<CommitModel> GetById(Guid commitId);

    /// <summary>
    /// Получение списка всех обработок магнитограмм
    /// </summary>
    /// <returns></returns>
    public Task<IEnumerable<CommitModel>> GetAll();

    /// <summary>
    /// Удаление обработки по идентификатору обработки
    /// </summary>
    public Task Delete(Guid commitId);
}