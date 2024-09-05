using Newtonsoft.Json;
using WebApi.DAL.Models.Implementation.Report;
using WebApi.DAL.Providers.Interface;

namespace WebApi.DAL.Providers.Implementation;

/// <summary>
/// Провайдер для работы с Report-сервисом
/// </summary>
public class ReportProvider : IReportProvider
{
    private readonly HttpClient _httpClient;
    private readonly string _url;

    public ReportProvider()
    {
        _url = Environment.GetEnvironmentVariable("REPORT_SERVICE_URL") ?? "http://localhost:8005";
        _httpClient = new HttpClient();
    }
    
    /// <summary>
    /// Метод получения отчета по идентификатору обработки
    /// </summary>
    public async Task<ReportEntity> GetReport(string commitId)
    {
        var response = await _httpClient.GetAsync($"{_url}/api/report/get");
        
        if(!response.IsSuccessStatusCode)
            throw new Exception(response.StatusCode.ToString());
        
        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<ReportEntity>(responseContent);
    }
}