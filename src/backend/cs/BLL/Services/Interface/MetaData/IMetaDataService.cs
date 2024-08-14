using BLL.Models.Implementation.MetaData;

namespace BLL.Services.Interface.MetaData;

public interface IMetaDataService
{
    public  Task Save(MetaDataModel metaData);

    public Task<MetaDataModel> Get(string metaDataId);

    public Task<IEnumerable<MetaDataModel>> GetAll();

    public Task Delete(string metaDataId);
}