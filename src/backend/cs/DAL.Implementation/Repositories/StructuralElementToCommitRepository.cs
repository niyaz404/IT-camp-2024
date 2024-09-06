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
}