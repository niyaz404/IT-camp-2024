using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Implementation.MetaData;

namespace WebApi.Controllers;

/// <summary>
/// Контроллер для работы с данными магнитограммы
/// </summary>
[ApiController]
[Route("[controller]/[action]")]
public class MetaDataController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Save(MetaDataDto metaDataDto)
    {
        var magtenogramId = "";
        return Ok(new { MagnetogramId = magtenogramId });
    }
    
    [HttpGet]
    public async Task<IActionResult> Get(string metaDataId)
    {
        return Ok(new { MagnetogramId = "magtenogramId" });
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(new { MagnetogramId = "magtenogramId" });
    }
    
    [HttpDelete]
    public async Task<IActionResult> Delete(string metaDataId)
    {
        return Ok(new { MagnetogramId = "magtenogramId" });
    }
}

