using Consts;
using DAL.Models.Implementation.Report;
using DAL.Repositories.Abstract;
using DAL.Repositories.Interface;
using Dapper;
using Npgsql;

namespace DAL.Implementation.Repositories;

/// <summary>
/// Репозиторий для работы с сущностью Report
/// </summary>
public class ReportRepository(string connectionString) : Repository(connectionString, PgTables.Report), IReportRepository
{
    public async Task Insert(ReportEntity report)
    {
        var sql = $"insert into {_mainTableName} (commitid, createdat, file) values(:commitId, :createdAt, :file)";
        
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.ExecuteAsync(sql,
            new { commitid = report.CommitId, createdAt = report.CreatedAt, file = report.File });
    }
    
    public async Task<ReportEntity> SelectByCommitId(Guid commitId)
    {
        var sql = $@"
            select id,
                   commitid,
                   createdat,
                   file
            from {_mainTableName} 
            where commitid=:commitId";

        await using var connection = new NpgsqlConnection(_connectionString);
        return await connection.QueryFirstOrDefaultAsync<ReportEntity>(sql, new { commitId });
    }
}