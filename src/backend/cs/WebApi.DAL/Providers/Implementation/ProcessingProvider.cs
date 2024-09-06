using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
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

    public ProcessingProvider(string url, HttpClient httClient)
    {
        _url = url;//Environment.GetEnvironmentVariable("REPORT_SERVICE_URL") ?? "http://localhost:8005";
        _httpClient = httClient;
    }
    
    /// <summary>
    /// Метод загрузки магнитограммы
    /// </summary>
    /// <returns>Идентификатор обработки (Commit)</returns>
    public async Task<string> SaveMagnetogram(MagnetogramEntity magnetogram)
    {
        var settings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };
        // Создаем HTTP-запрос для MLService
        var requestContent = new StringContent(JsonConvert.SerializeObject(magnetogram, settings), Encoding.UTF8,
            "application/json");
        var response = await _httpClient.PostAsync($"{_url}/ai/load", requestContent);
            
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(response.ReasonPhrase);
        }
            
        var responseContent = await response.Content.ReadAsStringAsync();
        var commitId = JsonConvert.DeserializeObject<dynamic>(responseContent).commitId;

        return commitId;
    }
}