using BLL.Models.Implementation.MetaData;

namespace BLL.Services.Interface.MetaData;

public interface IStructuralElementService
{
    Task<IEnumerable<DefectModel>> GetStrcturalElement(string[] elementIds);

    Task<DefectModel> GetStrcturalElement(string elementId);
}