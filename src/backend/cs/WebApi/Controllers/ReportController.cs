using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Implementation.Report;

namespace WebApi.Controllers;

/// <summary>
/// Контроллер для работы с отчетами
/// </summary>
[ApiController]
[Route("[controller]/[action]")]
public class ReportController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get(string metaDataId)
    {
        return Ok(new { File = new ReportDto() });
    }
}

