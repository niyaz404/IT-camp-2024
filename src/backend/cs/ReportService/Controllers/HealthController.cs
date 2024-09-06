using Microsoft.AspNetCore.Mvc;

namespace ReportService.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Check()
        {
            return Ok("Ok");
        }
    }
}