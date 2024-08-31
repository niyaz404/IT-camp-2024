using WebApi.BLL.Models.Implementation.Commit;

namespace WebApi.BLL.Services.Interface.Commit;

public interface ICommitService
{
    public Task Save(CommitModel commit);

    public Task<CommitModel> Get(string commitId);

    public Task<IEnumerable<CommitModel>> GetAll();

    public Task Delete(string commitId);
}