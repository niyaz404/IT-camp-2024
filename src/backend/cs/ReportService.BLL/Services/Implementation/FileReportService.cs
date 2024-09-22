using AutoMapper;
using DAL.Models.Implementation.Report;
using DAL.Repositories.Interface;
using ReportService.BLL.Helpers;
using ReportService.BLL.Models;
using ReportService.BLL.Report.Interface;
using ReportService.BLL.Services.Interface;

namespace ReportService.BLL.Services.Implementation;

/// <summary>
/// Сервис для работы с отчетами (приписка File лишь для устранения коллизии имен с проектом)
/// </summary>
public class FileReportService : IReportService
{
    private readonly IReportRepository _reportRepository;
    private readonly ICommitService _commitService;
    private readonly IReportGenerator _reportGenerator;
    private readonly IMapper _mapper;
        
    public FileReportService(
        IReportRepository reportRepository, 
        ICommitService commitService,
        IReportGenerator reportGenerator, 
        IMapper mapper)
    {
        _reportRepository = reportRepository;
        _commitService = commitService;
        _reportGenerator = reportGenerator;
        _mapper = mapper;
    }
    
    public async Task<ReportModel> GetByCommitId(Guid commitId)
    {
        var reportEntity = await _reportRepository.SelectByCommitId(commitId);
        
        if(reportEntity != null)
        {
            return _mapper.Map<ReportModel>(reportEntity);
        }

        var commit = await _commitService.GetById(commitId);
        
        var report = new ReportModel
        {
            File = _reportGenerator.GenerateCommitReport(commit),
            CreatedAt = DateTime.Now,
            CommitId = commitId
        };

        await _reportRepository.Insert(_mapper.Map<ReportEntity>(report));

        return report;
    }
}