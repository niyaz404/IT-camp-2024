using Newtonsoft.Json;
using WebApi.DAL.Models.Implementation.Commit;
using WebApi.DAL.Models.Implementation.Magnetogram;
using WebApi.DAL.Providers.Interface;

namespace WebApi.DAL.Providers.Implementation;

/// <summary>
/// Провайдер для работы с сервисом обработки
/// </summary>
public class ProcessingProvider : IProcessingProvider
{
    private readonly HttpClient _httpClient;
    private readonly string _url;

    public ProcessingProvider()
    {
        _url = Environment.GetEnvironmentVariable("PROCESSING_SERVICE_URL") ?? "http://localhost:8010";
        _httpClient = new HttpClient();
    }
    
    /// <summary>
    /// Метод получения отчета по идентификатору обработки
    /// </summary>
    public async Task<CommitEntity> Process(MagnetogramEntity magnetogram)
    {
        ////////////////////////////////////////////////////////////////////
        var response = await _httpClient.GetAsync($"{_url}/api/report/get");
        
        if(!response.IsSuccessStatusCode)
            throw new Exception(response.StatusCode.ToString());
        
        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<CommitEntity>(responseContent);
    }
}