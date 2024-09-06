using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Контроллер для проверки работоспособности сервиса
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class HealthController : ControllerBase
    {
        /// <summary>
        /// Проверка работоспособности сервиса
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<string>> Check()
        {
            return Ok("Ok");
        }
    }
}