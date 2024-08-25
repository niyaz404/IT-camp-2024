using BLL.Models.Implementation.MetaData;
using BLL.Services.Interface.MetaData;

namespace BLL.Services.Implementation.MetaData;

public class MetaDataService : IMetaDataService
{
    public async Task Save(MetaDataModel metaData)
    {
        return;
    }

    public async Task<MetaDataModel> Get(string metaDataId)
    {
        return;
    }

    public async Task<IEnumerable<MetaDataModel>> GetAll()
    {
        return;
    }

    public async Task Delete(string metaDataId)
    {
        return;
    }
}