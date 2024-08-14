using BLL.Models.Implementation.MetaData;
using BLL.Services.Interface.MetaData;

namespace BLL.Services.Implementation.MetaData;

public class MetaDataService : IMetaDataService
{
    public Task Save(MetaDataModel metaData)
    {
        return;
    }

    public Task<MetaDataModel> Get(string metaDataId)
    {
        return;
    }

    public Task<IEnumerable<MetaDataModel>> GetAll()
    {
        return;
    }

    public Task Delete(string metaDataId)
    {
        return;
    }
}