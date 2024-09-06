using WebApi.BLL.Models.Implementation.Commit;
using WebApi.BLL.Services.Interface.Commit;

namespace WebApi.BLL.Services.Implementation.Commit;

public class DefectService : IDefectService
{
    public Task<IEnumerable<DefectModel>> GetDefects(string[] defectIds)
    {
        return null;
    }

    public Task<DefectModel> GetDefect(string defectId)
    {
        return null;
    }
}