using System.Text;
using Newtonsoft.Json;
using WebApi.BLL.Models.Implementation.Auth;
using WebApi.DAL.Providers.Interface;

namespace WebApi.DAL.Providers.Implementation;

/// <summary>
/// Провайдер для работы с Auth-сервисом
/// </summary>
public class AuthProvider : IAuthProvider
{
    private readonly string _url;
    private readonly HttpClient _httpClient;
    public AuthProvider(string url, HttpClient httpClient)
    {
        _url = url;
        _httpClient = httpClient;
    }
    
    public async Task<string> Login(UserCredentials userCredentials)
    {
        // Создаем HTTP-запрос для AuthService
        var requestContent = new StringContent(JsonConvert.SerializeObject(userCredentials), Encoding.UTF8,
            "application/json");
        var response = await _httpClient.PostAsync($"{_url}/api/auth/generatetoken", requestContent);
            
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Invalid username or password.");
        }
            
        var responseContent = await response.Content.ReadAsStringAsync();
        var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(responseContent);

        return tokenResponse.Token;
    }

    /// <summary>
    /// Модель ответа от сервиса авторизации
    /// </summary>
    public class TokenResponse
    {
        public string Token { get; set; }
    }
}