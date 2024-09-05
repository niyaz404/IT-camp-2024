using AuthService.DAL.Models;

namespace AuthService.DAL.Repositories.Interface;

/// <summary>
/// Интерфейс репозитория сущности Пользователя
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Возвращает пользователя по логину
    /// </summary>
    /// <param name="login">Логин</param>
    public Task<UserEntity> SelectByLogin(string login);

    /// <summary>
    /// Добавляет нового пользователя в систему
    /// </summary>
    /// <param name="user">Сущность ползователя</param>
    public Task Insert(UserEntity user);
    
    /// <summary>
    /// Получение идентификатора пользователя по логину
    /// </summary>
    /// <param name="login">Логин ползователя</param>
    public Task<Guid> SelectIdByLogin(string login);
}