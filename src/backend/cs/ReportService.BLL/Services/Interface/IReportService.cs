using ReportService.BLL.Models;

namespace ReportService.BLL.Services.Interface;

/// <summary>
/// Интерфейс сервиса по работе с отчетами
/// </summary>
public interface IReportService
{
    /// <summary>
    /// Получение отчета по идентификатору обработки
    /// </summary>
    public Task<ReportModel> GetByCommitId(Guid commitId);
}