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
    private readonly ICommitRepository _commitRepository;
    private readonly IDefectRepository _defectRepository;
    private readonly IStructuralElementRepository _structuralElementRepository;
    private readonly IReportGenerator _reportGenerator;
    private readonly IMapper _mapper;
        
    public FileReportService(
        IReportRepository reportRepository, 
        ICommitRepository commitRepository,
        IDefectRepository defectRepository,
        IStructuralElementRepository structuralElementRepository,
        IReportGenerator reportGenerator, 
        IMapper mapper)
    {
        _reportRepository = reportRepository;
        _commitRepository = commitRepository;
        _defectRepository = defectRepository;
        _structuralElementRepository = structuralElementRepository;
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

        var commitEntity = await _commitRepository.SelectById(commitId);
        var commit = _mapper.Map<CommitModel>(commitEntity);
        commit.Defects = _mapper.Map<List<DefectModel>>(await _defectRepository.SelectByCommitId(commitId));
        commit.StructuralElements = _mapper.Map<List<StructuralElementModel>>(await _structuralElementRepository.SelectByCommitId(commitId));
        
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