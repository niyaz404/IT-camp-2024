using Consts;
using DAL.Models.Implementation.Commit;
using DAL.Repositories.Abstract;
using DAL.Repositories.Interface;
using Dapper;
using Npgsql;

namespace DAL.Implementation.Repositories;

/// <summary>
/// Репозиторий для работы с сущностью Commit
/// </summary>
public class CommitRepository(string connectionString) : Repository(connectionString, PgTables.Commit), ICommitRepository
{
    public async Task<CommitEntity> SelectById(Guid commitId)
    {
        var sql = $@"
            select id,
                magnetogramid,
                name,
                createdat,
                createdby,
                processedimage
            from {_mainTableName} 
            where id=:commitId";

        await using var connection = new NpgsqlConnection(_connectionString);
        return await connection.QueryFirstOrDefaultAsync<CommitEntity>(sql, new { commitId });
    }
}