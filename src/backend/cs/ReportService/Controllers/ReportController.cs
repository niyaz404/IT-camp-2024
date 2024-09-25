using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReportService.BLL.Services.Interface;
using ReportService.Models;

namespace ReportService.Controllers
{
    /// <summary>
    /// Контроллер для работы с отчетами
    /// </summary>
    [ApiController]
    [Route("[controller]/[action]")]
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
        /// Метод получения отчета
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get(Guid commitId)
        {
            try
            {
                var report = await _reportService.GetByCommitId(commitId);
                
                return Ok(_mapper.Map<ReportDto>(report));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                Console.WriteLine(e.Message);
                return BadRequest(e.Message);
            }
        }
    }
}