using ReportService.BLL.Models;

namespace ReportService.BLL.Report.Interface;

public interface IReportGenerator
{
    public byte[] GenerateCommitReport(CommitModel commit) => throw new NotImplementedException();
}