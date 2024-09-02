using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using WebApi.Models.Implementation.Auth;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public AuthController(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        
        /// <summary>
        /// Метод авторизации
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<string>> Login([FromBody] UserCredentialsDto userCredentials)
        {
            // Создаем HTTP-запрос для AuthService
            var requestContent = new StringContent(JsonConvert.SerializeObject(userCredentials), Encoding.UTF8, "application/json");
            
            var authServiceUrl = _configuration.GetValue<string>("AuthService:Url");
            var check = await _httpClient.GetAsync($"{authServiceUrl}/health/check");
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:5000/");
            
            // Настройка HttpClient, если требуется (например, заголовки)
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
            var ch = await httpClient.GetAsync("http://localhost:5000/health/check");
            var response = await _httpClient.PostAsync($"{authServiceUrl}/api/auth/generatetoken", requestContent);
            
            if (!response.IsSuccessStatusCode)
            {
                return Unauthorized("Invalid username or password.");
            }
            
            var responseContent = await response.Content.ReadAsStringAsync();
            var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(responseContent);
            
            // Проверка и обработка JWT-токена
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(tokenResponse.Token) as JwtSecurityToken;
            var role = jsonToken?.Claims.First(claim => claim.Type == "role").Value;

            return Ok(new { Token = tokenResponse.Token, Role = role });
        }
    }

    public class TokenResponse
    {
        public string Token { get; set; }
    }
}