using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authorization;

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
        /// OPTIONS-эндпойнт для браузера
        /// </summary>
        [HttpOptions("/api/auth/login")]
        public IActionResult Options()
        {
            return Ok();
        }
        
        /// <summary>
        /// Метод авторизации
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserCredentials userCredentials)
        {
            // Создаем HTTP-запрос для AuthService
            var requestContent = new StringContent(JsonConvert.SerializeObject(userCredentials), Encoding.UTF8, "application/json");

            var authServiceUrl = _configuration.GetValue<string>("AuthService:Url");
            var response = await _httpClient.PostAsync($"{authServiceUrl}/api/auth/token", requestContent);

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

            // Установка cookie или отправка токена в ответе
            return Ok(new { Token = tokenResponse.Token, Role = role });
        }
    }

    public class TokenResponse
    {
        public string Token { get; set; }
    }

    public class UserCredentials
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}