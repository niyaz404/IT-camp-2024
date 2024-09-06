using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using WebApi.DAL.Models.Implementation.Commit;
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

    public ReportProvider(string url, HttpClient httClient)
    {
        _url = url;//Environment.GetEnvironmentVariable("REPORT_SERVICE_URL") ?? "http://localhost:8005";
        _httpClient = httClient;
    }
    
    /// <summary>
    /// Метод получения отчета по идентификатору обработки
    /// </summary>
    public async Task<ReportEntity> GetReport(Guid commitId)
    {
        var response = await _httpClient.GetAsync($"{_url}/report/get?commitId={commitId}");
        
        if(!response.IsSuccessStatusCode)
            throw new Exception(response.StatusCode.ToString());
        
        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<ReportEntity>(responseContent);
    }

    public async Task<IEnumerable<CommitEntity>> GetAllCommits()
    {
        var response = await _httpClient.GetAsync($"{_url}/commit/getall");
        
        if(!response.IsSuccessStatusCode)
            throw new Exception(response.StatusCode.ToString());
        
        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<IEnumerable<CommitEntity>>(responseContent);
    }

    public async Task<CommitEntity> GetCommitById(Guid commitId)
    {
        var response = await _httpClient.GetAsync($"{_url}/commit/get?commitid={commitId}");
        
        if(!response.IsSuccessStatusCode)
            throw new Exception(response.StatusCode.ToString());
        
        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<CommitEntity>(responseContent);
    }

    public async Task SaveCommit(CommitEntity commit)
    {
        var requestContent = new StringContent(JsonConvert.SerializeObject(commit), Encoding.UTF8,
            "application/json");
        var response = await _httpClient.PostAsync($"{_url}/commit/save", requestContent);
            
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(response.ReasonPhrase);
        }
            
        var responseContent = await response.Content.ReadAsStringAsync();
        var commitEntity = JsonConvert.DeserializeObject<CommitEntity>(responseContent); 
    }

    public async Task DeleteCommit(Guid commitId)
    {
        var requestContent = new StringContent(JsonConvert.SerializeObject(commitId), Encoding.UTF8,
            "application/json");
        var response = await _httpClient.DeleteAsync($"{_url}/commit/delete?commitid={commitId}");
            
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(response.ReasonPhrase);
        } 
    }
}