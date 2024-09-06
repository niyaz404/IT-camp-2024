using WebApi.BLL.Models.Implementation.Commit;

namespace WebApi.BLL.Services.Interface.Commit;

public interface IStructuralElementService
{
    Task<IEnumerable<DefectModel>> GetStructuralElements(string[] elementIds);

    Task<DefectModel> GetStructuralElement(string elementId);
}