using WebApi.BLL.Models.Implementation.Commit;
using WebApi.BLL.Services.Interface.Commit;

namespace WebApi.BLL.Services.Implementation.Commit;

public class CommitService : ICommitService
{
    public async Task Save(CommitModel commit)
    {
        return;
    }

    public async Task<CommitModel> Get(string commitId)
    {
        return null;
    }

    public async Task<IEnumerable<CommitModel>> GetAll()
    {
        return null;
    }

    public async Task Delete(string commitId)
    {
        return;
    }
}