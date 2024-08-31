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
    public async Task<IActionResult> Get(string commitId)
    {
        var fileName = "1.pdf";
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);

        if (!System.IO.File.Exists(filePath))
        {
            return NotFound(); // Если файл не найден, возвращаем 404
        }

        // Читаем файл в массив байтов
        byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

        
        return Ok(new ReportDto()
        {
            Id = "sf",
            CommitId = commitId,
            CreatedAt = DateTime.Now,
            File = fileBytes
        });
    }
}

