using WebApi.DAL.Models.Implementation.Report;

namespace WebApi.DAL.Providers.Interface;

/// <summary>
/// Интерфейс провайдора для работы с Report-сервисом
/// </summary>
public interface IReportProvider
{
    public Task<ReportEntity> GetReport(string commitId);
}