using WebApi.BLL.Models.Implementation.Auth;

namespace WebApi.BLL.Services.Interface.Auth;

/// <summary>
/// Интерфейс сервиса для работы с авторизацией
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Метод входа в систему (получения токена)
    /// </summary>
    public Task<LoginResponseModel> Login(UserCredentialsModel userCredentials);
}