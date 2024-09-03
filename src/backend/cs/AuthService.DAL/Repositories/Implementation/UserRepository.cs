using AuthService.DAL.Models;
using AuthService.DAL.Repositories.Abstract;
using AuthService.DAL.Repositories.Interface;
using Dapper;
using Npgsql;

namespace AuthService.DAL.Repositories.Implementation;

/// <summary>
/// Репозиторий для сущностей Пользователя
/// </summary>
public class UserRepository(string connectionString) : Repository(connectionString, "user"), IUserRepository
{
    public async Task<UserEntity> SelectByLogin(string login)
    {
        var sql = $"select * from {_mainTableName} where login=:login";

        await using var connection = new NpgsqlConnection(_connectionString);
        return await connection.QueryFirstOrDefaultAsync<UserEntity>(sql, new { login });
    }

    public async Task Insert(UserEntity user)
    {
        var sql = $"insert into {_mainTableName} (username, login, hash, salt) values(:username, :login, :hash, :salt)";
        
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.ExecuteAsync(sql, new
        {
            username = user.Username,
            login = user.Login,
            hash = user.PasswordHash,
            salt = user.PasswordSalt
        });
    }
}