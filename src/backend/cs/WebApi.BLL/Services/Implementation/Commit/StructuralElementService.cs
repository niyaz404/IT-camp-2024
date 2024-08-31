using WebApi.BLL.Models.Implementation.Commit;
using WebApi.BLL.Services.Interface.Commit;

namespace WebApi.BLL.Services.Implementation.Commit;

public class StructuralElementService : IStructuralElementService
{
    public Task<IEnumerable<DefectModel>> GetStructuralElement(string[] elementIds)
    {
        return null;
    }

    public Task<DefectModel> GetStructuralElement(string elementId)
    {
        return null;
    }
}