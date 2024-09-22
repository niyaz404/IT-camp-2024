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

    public async Task<Guid> Insert(Guid commitId, Guid defectId)
    {
        var sql = $@"insert into {_mainTableName} 
                    (commitid, defectid) 
                    VALUES (:commitId, :defectId) 
                    returning id;";
        
        await using var connection = new NpgsqlConnection(_connectionString);
        return await connection.ExecuteScalarAsync<Guid>(sql, new
        {
            commitId, defectId
        });
    }

    public async Task DeleteByCommitId(Guid commitId)
    {
        var sql = $@"delete from {_mainTableName} 
                    where commitid = :commitId;";
        
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.ExecuteAsync(sql, new { commitId });
    }
}