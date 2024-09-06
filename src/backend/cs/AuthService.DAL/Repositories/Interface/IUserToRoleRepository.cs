using AuthService.DAL.Models;

namespace AuthService.DAL.Repositories.Interface;

/// <summary>
/// Интерфейс репозитория Связи пользователя и роли
/// </summary>
public interface IUserToRoleRepository
{
    /// <summary>
    /// Добавление связи пользователя и роли
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <param name="roleId">Идентификатор роли</param>
    public Task Insert(UserToRoleEntity userToRole);

    /// <summary>
    /// Получение идентификаторов ролей по идентификатору пользователя
    /// </summary>
    /// <param name="userId">Идентификатор пользовател</param>
    /// <returns>Список идентификаторов ролей</returns>
    public Task<IEnumerable<int>> SelectByUserId(string userId);
}