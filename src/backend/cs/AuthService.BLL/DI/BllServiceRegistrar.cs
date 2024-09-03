using AuthService.BLL.Services.Implementation;
using AuthService.BLL.Services.Interface;
using Unity;
using Unity.Injection;

namespace AuthService.BLL.DI;

/// <summary>
/// Регистратор сервисов слоя Бизнес-логики
/// </summary>
public static class BllServiceRegistrar
{
    /// <summary>
    /// Регистрация сервисов
    /// </summary>
    /// <param name="container"></param>
    public static void Register(IUnityContainer container)
    {
        // Регистрация сервисов
        container.RegisterType<IUserService, UserService>();
    }
}