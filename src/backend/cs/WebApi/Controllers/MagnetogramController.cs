using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.BLL.Models.Implementation.Magnetogram;
using WebApi.BLL.Services.Interface.Auth;
using WebApi.BLL.Services.Interface.Magnetogram;
using WebApi.Models.Implementation.Magnerogram;

namespace WebApi.Controllers;

/// <summary>
/// Контроллер для работы с магнитограммами
/// </summary>
[ApiController]
[Route("api/[controller]/[action]")]
public class MagnetogramController : ControllerBase
{
    private readonly IMagnetogramService _magntogramService;
    private readonly IMapper _mapper;

    public MagnetogramController(IMagnetogramService magnetogramService, IMapper mapper)
    {
        try
        {
            _magntogramService = magnetogramService;
            _mapper = mapper;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.StackTrace);
            Console.WriteLine(e.Message);
        }
    }
    
    /// <summary>
    /// Сохранение файла магнитограммы
    /// </summary>
    /// <param name="magnetogramDto">Магнитограмма</param>
    /// <returns>Идентификатор сохраненной магнитограммы</returns>
    [HttpPost]
    public async Task<ActionResult<string>> Save(MagnetogramDto magnetogramDto)
    {
        try
        {
            var magnetogramId = await _magntogramService.SaveMagnetogram(_mapper.Map<MagnetogramModel>(magnetogramDto));
            return Ok(magnetogramId);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.StackTrace);
            Console.WriteLine(e.Message);
            return BadRequest(e.Message);
        }
    }
}