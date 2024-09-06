using AuthService.BLL.Models;

namespace AuthService.BLL.Services.Interface;

/// <summary>
/// Сервис аутентификации на уровне бизнес-логики
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Генерирует токен доступа
    /// </summary>
    public Task<string> GenerateToken(UserCredentialsModel userCredentials);

    /// <summary>
    /// Регистрирует пользователя в системе
    /// </summary>
    public Task Register(UserModel user);
}