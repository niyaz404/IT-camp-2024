using Consts;
using DAL.Repositories.Abstract;
using DAL.Repositories.Interface;
using Dapper;
using Npgsql;

namespace DAL.Implementation.Repositories;

/// <summary>
/// Репозиторий для работы с сущностью Defect
/// </summary>
public class DefectToCommitRepository(string connectionString) : Repository(connectionString, PgTables.DefectToCommit), IDefectToCommitRepository
{
    public async Task<IEnumerable<Guid>> SelectDefectIdsByCommitIds(Guid commitId)
    {
        var sql = $"select defectid from {_mainTableName} where commitid=:commitId";

        await using var connection = new NpgsqlConnection(_connectionString);
        return await connection.QueryAsync<Guid>(sql, new { commitId });
    }
}