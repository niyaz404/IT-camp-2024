using Consts;
using DAL.Models.Implementation.Commit;
using DAL.Repositories.Abstract;
using DAL.Repositories.Interface;
using Dapper;
using Npgsql;

namespace DAL.Implementation.Repositories;

/// <summary>
/// Репозиторий для работы с сущностью Defect
/// </summary>
public class DefectRepository : Repository, IDefectRepository
{
    private IDefectToCommitRepository _defectToCommitRepository;
    
    public DefectRepository(string connectionString, IDefectToCommitRepository defectToCommitRepository) 
        : base(connectionString, PgTables.Defect)
    {
        _defectToCommitRepository = defectToCommitRepository;
    }
    
    public async Task<IEnumerable<DefectEntity>> SelectByCommitId(Guid commitId)
    {
        var defectIds = await _defectToCommitRepository.SelectDefectIdsByCommitIds(commitId);
        
        var sql = $@"
            select id,
                description,
                startx as startxcoordinate,
                endx as endxcoordinate
            from {_mainTableName} 
            where id = any(:defectIds)";

        await using var connection = new NpgsqlConnection(_connectionString);
        return await connection.QueryAsync<DefectEntity>(sql, new { defectIds });
    }

    public async Task<Guid> Insert(DefectEntity defect)
    {
        var sql = $@"insert into {_mainTableName} 
                    (description, startx, endx) 
                    VALUES (:description, :startxcoordinate, :endxcoordinate) 
                    returning id;";
        
        await using var connection = new NpgsqlConnection(_connectionString);
        return await connection.ExecuteScalarAsync<Guid>(sql, new
        {
            defect.Description, defect.StartXCoordinate, defect.EndXCoordinate
        });
    }
}