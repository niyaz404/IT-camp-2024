using BLL.Models.Implementation.Report;

namespace BLL.Services.Interface.Report;

public interface IReportService
{
    Task<ReportModel> Get(string metaDataId);
}