using AuthService.BLL.DI;
using AuthService.DAL.DI;
using Unity;

namespace AuthService.DI;

public static class UnityConfig
{
    public static void ConfigureServices(IServiceCollection services)
    {
        var container = new UnityContainer();
           
        // Регистрация сервисов DLL
        DalServiceRegistrar.Register(container);
        // Регистрация сервисов BLL
        BllServiceRegistrar.Register(container);
    }
}