using BLL.Models.Implementation.MetaData;
using BLL.Services.Interface.MetaData;

namespace BLL.Services.Implementation.MetaData;

public class DefectService : IDefectService
{
    public Task<IEnumerable<DefectModel>> GetDefects(string[] defectIds)
    {
        return;
    }

    public Task<DefectModel> GetDefect(string defectId)
    {
        return;
    }
}