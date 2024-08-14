using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Implementation.Magnerogram;

namespace WebApi.Controllers;

/// <summary>
/// Контроллер для работы с магнитограммами
/// </summary>
[ApiController]
[Route("[controller]/[action]")]
public class MagnetogramController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Save(MagnetogramDto magnetogramDto)
    {
        return Ok(new { MagnetogramId = "magtenogramId" });
    }
}