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
        await _commitService.Save(_mapper.Map<CommitModel>(commitDto));
        return Ok();
    }
    
    /// <summary>
    /// Получение обработки магнитограммы
    /// </summary>
    /// <param name="commitId">Идентификатор обработки</param>
    [HttpGet]
    public async Task<ActionResult<CommitDto>> Get(Guid commitId)
    {
        var commit = _mapper.Map<CommitDto>(await _commitService.Get(commitId));
        commit.ProcessedImage =
            "https://psv4.userapi.com/c909628/u181754921/docs/d2/8e788a45ca20/magnetogram_output.png?extra=xHS1L4ScAeKZTx3iUM_Opaq1Y8szqK5ceTOuXC3L2PSIHkUySG50jdprdMsDq4XqGhJhl22Dt293LZAaz5uIBxuJfo38uKMWSJfxQ6QrVekhi_lo1CdVb_DrBj7NtXGGHKA9ycE6sKizbpR1zDEGig6C4Ek";
        return Ok(commit);
    }
    
    /// <summary>
    /// Получение все обработок магнитограмм
    /// </summary>
    /// <returns>Список обработок</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CommitDto>>> GetAll()
    {
        var result = _mapper.Map<IEnumerable<CommitDto>>(await _commitService.GetAll());
        return Ok(result);
    }
    
    /// <summary>
    /// Удаление обработки магнитограммы
    /// </summary>
    /// <param name="commitId">Идентификатор обработки</param>
    /// <returns>Статус операции</returns>
    [HttpDelete]
    public async Task<ActionResult<bool>> Delete(string commitId)
    {
        return Ok(true);
    }
}

