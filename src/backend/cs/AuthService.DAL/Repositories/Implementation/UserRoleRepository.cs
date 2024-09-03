using AuthService.DAL.Repositories.Abstract;
using AuthService.DAL.Repositories.Interface;
using Dapper;
using Npgsql;

namespace AuthService.DAL.Repositories.Implementation;

/// <summary>
/// Репозиторий для Связи Пользователя и Роли
/// </summary>
public class UserRoleRepository(string connectionString) : Repository(connectionString, "user_role"), IUserRoleRepository
{
    public async Task Insert(string userId, int roleId)
    {
        var sql = $"insert into {_mainTableName} (userid, roleid) values(:userid, :roleid)";
        
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.ExecuteAsync(sql, new { userid = userId, roleid = roleId});
    }

    public async Task<IEnumerable<int>> SelectByUserId(string userId)
    {
        var sql = $"select roleid from {_mainTableName} where userid = :userid";
        
        await using var connection = new NpgsqlConnection(_connectionString);
        return await connection.QueryAsync<int>(sql, new { userid = userId });
    }
}