using Consts;
using DAL.Models.Implementation.Commit;
using DAL.Repositories.Abstract;
using DAL.Repositories.Interface;
using Dapper;
using Npgsql;

namespace DAL.Implementation.Repositories;

/// <summary>
/// Репозиторий для работы с сущностью StructuralElement_Commit
/// </summary>
public class StructuralElementToCommitRepository(string connectionString)
    : Repository(connectionString, PgTables.StructuralElementToCommit), IStructuralElementToCommitRepository
{
    public async Task<IEnumerable<Guid>> SelectStructuralElementIdsByCommitIds(Guid commitId)
    {
        var sql = $"select structuralelementid from {_mainTableName} where commitid=:commitId";

        await using var connection = new NpgsqlConnection(_connectionString);
        return await connection.QueryAsync<Guid>(sql, new { commitId });
    }

    public async Task<Guid> Insert(Guid commitId, Guid structuralElementId)
    {
        var sql = $@"insert into {_mainTableName} 
                    (commitid, structuralelementid) 
                    VALUES (:commitId, :structuralElementId) 
                    returning id;";
        
        await using var connection = new NpgsqlConnection(_connectionString);
        return await connection.ExecuteScalarAsync<Guid>(sql, new
        {
            commitId, structuralElementId
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