using WebApi.BLL.Models.Implementation.Report;

namespace WebApi.BLL.Services.Interface.Report;

public interface IReportService
{
    Task<ReportModel> Get(string commitId);
}