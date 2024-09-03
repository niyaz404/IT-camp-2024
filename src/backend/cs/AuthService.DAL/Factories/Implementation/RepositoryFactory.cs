using AuthService.DAL.Factories.Interface;
using AuthService.DAL.Repositories.Implementation;
using AuthService.DAL.Repositories.Interface;

namespace AuthService.DAL.Factories.Implementation;

public class RepositoryFactory(string connectionString) : IRepositoryFactory
{
    public IUserRepository CreateUserRepository()
    {
        return new UserRepository(connectionString);
    }

    public IUserRoleRepository CreateUserRoleRepository()
    {
        return new UserRoleRepository(connectionString);
    }
}