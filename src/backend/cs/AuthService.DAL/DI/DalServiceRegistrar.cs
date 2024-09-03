using AuthService.DAL.Factories.Implementation;
using AuthService.DAL.Factories.Interface;
using AuthService.DAL.Repositories.Interface;
using Unity;

namespace AuthService.DAL.DI;

public static class DalServiceRegistrar
{
    public static void Register(IUnityContainer container)
    {
        // Регистрация строки подключения
        var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING") ??
                               "Host=auth_postgres_db;Port=5432;Username=auth;Password=password;Database=auth;";

        // Регистрация фабрики
        container.RegisterFactory<IRepositoryFactory>(c =>
            new RepositoryFactory(connectionString));

        // Регистрация репозиториев через фабрику
        container.RegisterFactory<IUserRepository>(c =>
            c.Resolve<IRepositoryFactory>().CreateUserRepository());
        container.RegisterFactory<IUserRoleRepository>(c =>
            c.Resolve<IRepositoryFactory>().CreateUserRoleRepository());
    }
}