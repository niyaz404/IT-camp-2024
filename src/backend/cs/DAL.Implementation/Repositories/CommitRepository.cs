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
    public async Task<IEnumerable<CommitEntity>> Select()
    {
        var sql = $@"
            select c.id,
                magnetogramid,
                name,
                createdat,
                createdby,
                processedimage,
                originalimage,
                count(dc.id) as isdefective
            from {_mainTableName} c
            join itcamp.defect_commit dc on dc.commitid = c.id
            group by c.id, magnetogramid, name, createdat, createdby, processedimage";

        await using var connection = new NpgsqlConnection(_connectionString);
        return await connection.QueryAsync<CommitEntity>(sql);
    }
    
    public async Task<CommitEntity> SelectById(Guid commitId)
    {
        var sql = $@"
            select c.id,
                magnetogramid,
                name,
                createdat,
                createdby,
                processedimage,
                originalimage,
                count(dc.id) as isdefective
            from {_mainTableName} c
            join itcamp.defect_commit dc on dc.commitid = c.id 
            where c.id=:commitId
            group by c.id, magnetogramid, name, createdat, createdby, processedimage";

        await using var connection = new NpgsqlConnection(_connectionString);
        return await connection.QueryFirstOrDefaultAsync<CommitEntity>(sql, new { commitId });
    }

    public async Task<Guid> Insert(CommitEntity commit)
    {
        var sql = $@"insert into {_mainTableName} 
                    (magnetogramid, name, createdat, createdby, processedimage, originalimage) 
                    VALUES (:magnetogramId, :name, :createdat, :createdby, :processedimage, :originalimage) 
                    returning id;";
        
        await using var connection = new NpgsqlConnection(_connectionString);
        return await connection.ExecuteScalarAsync<Guid>(sql, new
        {
            commit.MagnetogramId, commit.Name, commit.CreatedAt, commit.CreatedBy, processedimage = commit.ProcessedImage.Length > 0 ? commit.ProcessedImage : null,
            originalimage = commit.OriginalImage.Length > 0 ? commit.OriginalImage : null
        });
    }

    public async Task Delete(Guid commitId)
    {
        var sql = $@"delete from {_mainTableName} 
                    where id = :commitId;";
        
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.ExecuteAsync(sql, new { commitId });
    }
}