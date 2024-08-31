using WebApi.BLL.Models.Implementation.Commit;

namespace WebApi.BLL.Services.Interface.Commit;

public interface IDefectService
{
    Task<IEnumerable<DefectModel>> GetDefects(string[] defectIds);

    Task<DefectModel> GetDefect(string defectId);
}