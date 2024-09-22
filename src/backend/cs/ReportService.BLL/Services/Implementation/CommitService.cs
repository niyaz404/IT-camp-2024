using AutoMapper;
using DAL.Models.Implementation.Commit;
using DAL.Repositories.Interface;
using ReportService.BLL.Models;
using ReportService.BLL.Services.Interface;

namespace ReportService.BLL.Services.Implementation;

/// <summary>
/// Сервис для работы с Обработками магнитограмм
/// </summary>
public class CommitService : ICommitService
{
    private readonly ICommitRepository _commitRepository;
    private readonly IDefectRepository _defectRepository;
    private readonly IStructuralElementRepository _structuralElementRepository;
    private readonly IDefectToCommitRepository _defectToCommitRepository;
    private readonly IStructuralElementToCommitRepository _structuralElementToCommitRepository;
    private readonly IReportRepository _reportRepository;
    private readonly IMapper _mapper;

    public CommitService(ICommitRepository commitRepository, 
        IDefectRepository defectRepository,
        IStructuralElementRepository structuralElementRepository, 
        IDefectToCommitRepository defectToCommitRepository,
        IStructuralElementToCommitRepository structuralElementToCommitRepository,
        IReportRepository reportRepository,
        IMapper mapper)
    {
        _commitRepository = commitRepository;
        _defectRepository = defectRepository;
        _structuralElementRepository = structuralElementRepository;
        _defectToCommitRepository = defectToCommitRepository;
        _structuralElementToCommitRepository = structuralElementToCommitRepository;
        _reportRepository = reportRepository;
        _mapper = mapper;
    }
    
    public async Task<Guid> Save(CommitModel commit)
    {
        var commitId = await _commitRepository.Insert(_mapper.Map<CommitEntity>(commit));

        foreach (var defect in commit.Defects)
        {
            var defectId = await _defectRepository.Insert(_mapper.Map<DefectEntity>(defect));
            await _defectToCommitRepository.Insert(commitId, defectId);
        }
        
        foreach (var element in commit.StructuralElements)
        {
            var elementId = await _structuralElementRepository.Insert(_mapper.Map<StructuralElementEntity>(element));
            await _structuralElementToCommitRepository.Insert(commitId, elementId);
        }
        
        return commitId;
    }

    public async Task<CommitModel> GetById(Guid commitId)
    {
        var commitEntity = await _commitRepository.SelectById(commitId);
        var commit = _mapper.Map<CommitModel>(commitEntity);
        commit.Defects = _mapper.Map<List<DefectModel>>(await _defectRepository.SelectByCommitId(commitId));
        commit.StructuralElements = _mapper.Map<List<StructuralElementModel>>(await _structuralElementRepository.SelectByCommitId(commitId));

        return commit;
    }

    public async Task<IEnumerable<CommitModel>> GetAll()
    {
        var commitEntities = await _commitRepository.Select();
        
        return _mapper.Map<IEnumerable<CommitModel>>(commitEntities);
    }

    public async Task Delete(Guid commitId)
    {
        //TODO: остаются мусорные дефекты и элементы, тоже нужно удалить
        //TODO: + засунуть в транзакцию
        await _defectToCommitRepository.DeleteByCommitId(commitId);
        await _structuralElementToCommitRepository.DeleteByCommitId(commitId);
        await _reportRepository.DeleteByCommitId(commitId);
        await _commitRepository.Delete(commitId);
    }
}