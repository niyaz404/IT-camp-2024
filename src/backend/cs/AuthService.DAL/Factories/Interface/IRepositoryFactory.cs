using AuthService.DAL.Repositories.Interface;

namespace AuthService.DAL.Factories.Interface;

/// <summary>
/// Интерфейс фабрики для создания репозиториев
/// </summary>
public interface IRepositoryFactory
{
    /// <summary>
    /// Метод создания экземпляра USerRepository
    /// </summary>
    IUserRepository CreateUserRepository();
    
    /// <summary>
    /// Метод создания экземпляра UserRoleRepository
    /// </summary>
    /// <returns></returns>
    IUserRoleRepository CreateUserRoleRepository();
}