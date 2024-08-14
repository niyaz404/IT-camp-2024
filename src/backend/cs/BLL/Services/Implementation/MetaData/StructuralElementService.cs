using BLL.Models.Implementation.MetaData;
using BLL.Services.Interface.MetaData;

namespace BLL.Services.Implementation.MetaData;

public class StructuralElementService : IStructuralElementService
{
    public Task<IEnumerable<DefectModel>> GetStrcturalElement(string[] elementIds)
    {
        return;
    }

    public Task<DefectModel> GetStrcturalElement(string elementId)
    {
        return;
    }
}