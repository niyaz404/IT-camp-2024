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
    private readonly IMapper _mapper;

    public CommitService(ICommitRepository commitRepository, 
        IDefectRepository defectRepository,
        IStructuralElementRepository structuralElementRepository, 
        IMapper mapper)
    {
        _commitRepository = commitRepository;
        _defectRepository = defectRepository;
        _structuralElementRepository = structuralElementRepository;
        _mapper = mapper;
    }
    
    public async Task<Guid> Save(CommitModel commit)
    {
        return await _commitRepository.Insert(_mapper.Map<CommitEntity>(commit));
    }

    public async Task<CommitModel> GetById(Guid commitId)
    {
        var commitEntity = await _commitRepository.SelectById(commitId);
        var commit = _mapper.Map<CommitModel>(commitEntity);
        commit.Defects = _mapper.Map<List<DefectModel>>(await _defectRepository.SelectByCommitId(commitId));
        commit.StructuralElements = _mapper.Map<List<StructuralElementModel>>(await _structuralElementRepository.SelectByCommitId(commitId));

        return _mapper.Map<CommitModel>(commitEntity);
    }

    public async Task<IEnumerable<CommitModel>> GetAll()
    {
        var commitEntities = await _commitRepository.Select();
        return _mapper.Map<IEnumerable<CommitModel>>(commitEntities);
    }

    public async Task Delete(Guid commitId)
    {
        await _commitRepository.Delete(commitId);
    }
}