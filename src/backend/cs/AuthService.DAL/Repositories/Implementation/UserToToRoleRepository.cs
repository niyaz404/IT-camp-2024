using AuthService.DAL.Models;
using AuthService.DAL.Repositories.Abstract;
using AuthService.DAL.Repositories.Interface;
using Dapper;
using Npgsql;

namespace AuthService.DAL.Repositories.Implementation;

/// <summary>
/// Репозиторий для Связи Пользователя и Роли
/// </summary>
public class UserToToRoleRepository(string connectionString) : Repository(connectionString, "user_role"), IUserToRoleRepository
{
    public async Task Insert(UserToRoleEntity userToRole)
    {
        var sql = $"insert into {_mainTableName} (userid, roleid) values(:userid, :roleid)";
        
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.ExecuteAsync(sql, new { userid = userToRole.UserId, roleid = userToRole.RoleId });
    }

    public async Task<IEnumerable<int>> SelectByUserId(string userId)
    {
        var sql = $"select roleid from {_mainTableName} where userid = :userid";
        
        await using var connection = new NpgsqlConnection(_connectionString);
        return await connection.QueryAsync<int>(sql, new { userid = userId });
    }
}