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
    public class CommitController : ControllerBase
    {
        private readonly ICommitService _commitService;
        private readonly IMapper _mapper;
    
        public CommitController(ICommitService commitService, IMapper mapper)
        {
            _commitService = commitService;
            _mapper = mapper;
        }
        
        /// <summary>
        /// Метод получения обработки магнитограммы
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get(Guid commitId)
        {
            try
            {
                var commit = await _commitService.GetById(commitId);
                
                return Ok(_mapper.Map<CommitDto>(commit));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        /// <summary>
        /// Метод получения списка обработок магнитограмм
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var commits = await _commitService.GetAll();
                
                return Ok(_mapper.Map<IEnumerable<CommitDto>>(commits));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        
        /// <summary>
        /// Метод удаления обработки магнитограммы
        /// </summary>
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid commitId)
        {
            try
            {
                await _commitService.Delete(commitId);
                
                return Ok("Commit deleted");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}