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
        //Хз зачем я это сюда засунул TODO: объединить запрос
        var structuralElementIds = await _structuralElementToCommitRepository.SelectStructuralElementIdsByCommitIds(commitId);
        
        var sql = $@"
            select se.id,
                   se.elementtypeid as typeid,
                   t.name as typename,
                   se.startx as startxcoordinate,
                   se.endx as endxcoordinate
            from {_mainTableName} se
            join itcamp.structuralelementtype t on se.elementtypeid = t.id
            where se.id = any(:structuralElementIds)";
    
        await using var connection = new NpgsqlConnection(_connectionString);
        return await connection.QueryAsync<StructuralElementEntity>(sql, new { structuralElementIds });
    }

    public async Task<Guid> Insert(StructuralElementEntity element)
    {
        var sql = $@"insert into {_mainTableName} 
                    (elementtypeid, startx, endx) 
                    VALUES (:typeid, :startxcoordinate, :endxcoordinate) 
                    returning id;";
        
        await using var connection = new NpgsqlConnection(_connectionString);
        return await connection.ExecuteScalarAsync<Guid>(sql, new
        {
            element.TypeId, element.StartXCoordinate, element.EndXCoordinate
        });
    }
}