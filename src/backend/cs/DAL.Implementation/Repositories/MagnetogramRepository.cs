using Consts;
using DAL.Repositories.Abstract;
using DAL.Repositories.Interface;

namespace DAL.Implementation.Repositories;

/// <summary>
/// Репозиторий для работы с сущностью Magnetogram
/// </summary>
public class MagnetogramRepository(string connectionString) : Repository(connectionString, PgTables.Magnetogram), IMagnetogramRepository
{
    
}