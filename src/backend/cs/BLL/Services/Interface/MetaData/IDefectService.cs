using BLL.Models.Implementation.MetaData;

namespace BLL.Services.Interface.MetaData;

public interface IDefectService
{
    Task<IEnumerable<DefectModel>> GetDefects(string[] defectIds);

    Task<DefectModel> GetDefect(string defectId);
}