using WebApi.BLL.Models.Implementation.Report;
using WebApi.BLL.Services.Interface.Report;

namespace WebApi.BLL.Services.Implementation.Report;

public class ReportService : IReportService
{
    public async Task<ReportModel> Get(string commitId)
    {
        return new ReportModel();
    }
}