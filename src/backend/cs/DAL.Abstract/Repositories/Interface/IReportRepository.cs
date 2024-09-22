using DAL.Models.Implementation.Commit;
using DAL.Models.Implementation.Report;

namespace DAL.Repositories.Interface;

/// <summary>
/// Интерфейс репозитория для работы с сущностью Report
/// </summary>
public interface IReportRepository
{
    /// <summary>
    /// Получение отчета по идентификатору обработки
    /// </summary>
    /// <param name="commitId">Идентификатор обработки</param>
    public Task<ReportEntity> SelectByCommitId(Guid commitId);

    /// <summary>
    /// Добавление записи в таблицу
    /// </summary>
    /// <param name="report"></param>
    public Task Insert(ReportEntity report);
    
    /// <summary>
    /// Удаляет запись по идентификатору обработки
    /// </summary>
    public Task DeleteByCommitId(Guid commitId);
}