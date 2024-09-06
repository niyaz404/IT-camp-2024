using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.BLL.Services.Interface.Report;
using WebApi.Models.Implementation.Report;

namespace WebApi.Controllers;

/// <summary>
/// Контроллер для работы с отчетами
/// </summary>
[ApiController]
[Route("api/[controller]/[action]")]
public class ReportController : ControllerBase
{
    private readonly IReportService _reportService;
    private readonly IMapper _mapper;
    public ReportController(IReportService reportService, IMapper mapper)
    {
        _reportService = reportService;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Получение отчета по идентификатору обработки
    /// </summary>
    /// <param name="commitId">Идентификатор обработки</param>
    /// <returns>Отчет</returns>
    [HttpGet]
    public async Task<ActionResult> Get(Guid commitId)
    {
        var report = await _reportService.Get(commitId);
        var file = File(report.File, "application/pdf", "report.pdf");


        return file;
    }
}

