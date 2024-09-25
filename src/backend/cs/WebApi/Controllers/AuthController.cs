using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using AutoMapper;
using WebApi.BLL.Models.Implementation.Auth;
using WebApi.BLL.Services.Interface.Auth;
using WebApi.Models.Implementation.Auth;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Контроллер авторизации
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public AuthController( IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }
        
        /// <summary>
        /// Метод авторизации
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<string>> Login([FromBody] UserCredentialsDto userCredentials)
        {
            try
            {
                var result = await _authService.Login(_mapper.Map<UserCredentialsModel>(userCredentials));
                return result.Token;
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