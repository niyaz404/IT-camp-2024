using WebApi.BLL.Models.Implementation.Auth;

namespace WebApi.DAL.Providers.Interface;

/// <summary>
/// Интерфейс провайдера для работы с Auth-сервисом
/// </summary>
public interface IAuthProvider
{
    /// <summary>
    /// Метод авторизации через сервис авторизации
    /// </summary>
    /// <returns>Токен</returns>
    public Task<string> Login(UserCredentials userCredentials);
}