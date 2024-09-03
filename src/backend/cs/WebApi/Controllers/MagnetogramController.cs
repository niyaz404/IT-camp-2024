using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Implementation.Magnerogram;

namespace WebApi.Controllers;

/// <summary>
/// Контроллер для работы с магнитограммами
/// </summary>
[ApiController]
[Route("api/[controller]/[action]")]
public class MagnetogramController : ControllerBase
{
    /// <summary>
    /// Сохранение файла магнитограммы
    /// </summary>
    /// <param name="magnetogramDto">Магнитограмма</param>
    /// <returns>Идентификатор сохраненной магнитограммы</returns>
    [HttpPost]
    public async Task<ActionResult<string>> Save(MagnetogramDto magnetogramDto)
    {
        return Ok("magtenogramId");
    }
}