using Consts;
using DAL.Models.Implementation.Commit;
using DAL.Repositories.Abstract;
using DAL.Repositories.Interface;
using Dapper;
using Npgsql;

namespace DAL.Implementation.Repositories;

/// <summary>
/// Репозиторий для работы с сущностью StructuralElement
/// </summary>
public class StructuralElementRepository : Repository, IStructuralElementRepository
{
    private IStructuralElementToCommitRepository _structuralElementToCommitRepository;
        
    public StructuralElementRepository(
        string connectionString, 
        IStructuralElementToCommitRepository structuralElementToCommitRepository) 
        : base(connectionString, PgTables.StructuralElement)
    {
        _structuralElementToCommitRepository = structuralElementToCommitRepository;
    }
        
    public async Task<IEnumerable<StructuralElementEntity>> SelectByCommitId(Guid commitId)
    {
        var structuralElementIds = await _structuralElementToCommitRepository.SelectStructuralElementIdsByCommitIds(commitId);
            
        var sql = $@"
            select id,
                   elementtypeid as structuralelementtypeid,
                   startx as startxcoordinate,
                   endx as endxcoordinate
            from {_mainTableName} 
            where id = any(:structuralElementIds)";
    
        await using var connection = new NpgsqlConnection(_connectionString);
        return await connection.QueryAsync<StructuralElementEntity>(sql, new { structuralElementIds });
    }
}