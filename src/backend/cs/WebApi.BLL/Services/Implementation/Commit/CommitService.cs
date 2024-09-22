using AutoMapper;
using WebApi.BLL.Models.Implementation.Commit;
using WebApi.BLL.Services.Interface.Commit;
using WebApi.DAL.Models.Implementation.Commit;
using WebApi.DAL.Providers.Interface;

namespace WebApi.BLL.Services.Implementation.Commit;

/// <summary>
/// Сервис для работы с фиксациями обработок
/// </summary>
public class CommitService : ICommitService
{
    private readonly IReportProvider _reportProvider;
    private readonly IMapper _mapper;
    
    public CommitService(IReportProvider reportProvider, IMapper mapper)
    {
        _reportProvider = reportProvider;
        _mapper = mapper;
    }
    public async Task Save(CommitModel commit)
    {
        await _reportProvider.SaveCommit(_mapper.Map<CommitEntity>(commit));
    }

    public async Task<CommitModel> Get(Guid commitId)
    {
        var commitEntity = await _reportProvider.GetCommitById(commitId);
        return _mapper.Map<CommitModel>(commitEntity);
    }

    public async Task<IEnumerable<CommitModel>> GetAll()
    {
        var commitEntities = await _reportProvider.GetAllCommits();
        var result = _mapper.Map<IEnumerable<CommitModel>>(commitEntities);
        return result;
    }

    public async Task Delete(Guid commitId)
    {
        await _reportProvider.DeleteCommit(commitId);
    }
}