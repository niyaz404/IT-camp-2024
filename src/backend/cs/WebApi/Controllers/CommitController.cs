using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.BLL.Models.Implementation.Commit;
using WebApi.BLL.Services.Interface.Commit;
using WebApi.Enums;
using WebApi.Models.Implementation.Commit;

namespace WebApi.Controllers;

/// <summary>
/// Контроллер для работы с данными магнитограммы
/// </summary>
[ApiController]
[Route("api/[controller]/[action]")]
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
    /// Сохранение обработки магнитограммы
    /// </summary>
    /// <returns>Идентификатор сущности обработки</returns>
    [HttpPost]
    public async Task<ActionResult<string>> Save([FromBody]CommitDto commitDto)
    {
        try
        {
            await _commitService.Save(_mapper.Map<CommitModel>(commitDto));
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    /// <summary>
    /// Получение обработки магнитограммы
    /// </summary>
    /// <param name="commitId">Идентификатор обработки</param>
    [HttpGet]
    public async Task<ActionResult<CommitDto>> Get(Guid commitId)
    {
        try
        {
            var commit = _mapper.Map<CommitDto>(await _commitService.Get(commitId));
            //commit.ProcessedImage =
                // "https://sun9-23.userapi.com/impg/kJ22PWyYRcZ0V-Lkww_tFPZnLLgt8gI5RBJISw/YRQLsjAcg9Y.jpg?size=2560x80&quality=96&sign=6e3186a60a5911af4c61da750079c477&type=album";
            return Ok(commit);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    /// <summary>
    /// Получение все обработок магнитограмм
    /// </summary>
    /// <returns>Список обработок</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CommitDto>>> GetAll()
    {
        try
        {
            var result = _mapper.Map<IEnumerable<CommitDto>>(await _commitService.GetAll());
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    /// <summary>
    /// Удаление обработки магнитограммы
    /// </summary>
    /// <param name="commitId">Идентификатор обработки</param>
    /// <returns>Статус операции</returns>
    [HttpDelete]
    public async Task<ActionResult<bool>> Delete(Guid commitId)
    {
        try
        {
            await _commitService.Delete(commitId);
            return Ok(true);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}

