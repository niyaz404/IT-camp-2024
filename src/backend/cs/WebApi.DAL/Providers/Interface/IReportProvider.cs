using WebApi.DAL.Models.Implementation.Commit;
using WebApi.DAL.Models.Implementation.Report;

namespace WebApi.DAL.Providers.Interface;

/// <summary>
/// Интерфейс провайдора для работы с Report-сервисом
/// </summary>
public interface IReportProvider
{
    /// <summary>
    /// Получение отчета по идентификатору обработки
    /// </summary>
    public Task<ReportEntity> GetReport(string commitId);

    /// <summary>
    /// Получение списка всех обработок магнитограмм
    /// </summary>
    public Task<IEnumerable<CommitEntity>> GetAllCommits();

    /// <summary>
    /// Получение обработки магнитограммы по идентификатору обработки
    /// </summary>
    public Task<CommitEntity> GetCommitById(Guid commitId);
}