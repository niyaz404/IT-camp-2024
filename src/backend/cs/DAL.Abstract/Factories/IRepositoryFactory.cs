using DAL.Repositories.Interface;

namespace DAL.Factories;

/// <summary>
/// Интерфейс фабрики для создания репозиториев
/// </summary>
public interface IRepositoryFactory
{
    /// <summary>
    /// Метод создания экземпляра CommitRepository
    /// </summary>
    public ICommitRepository CreateCommitRepository();
    
    /// <summary>
    /// Метод создания экземпляра DefectRepository
    /// </summary>
    public IDefectRepository CreateDefectRepository();

    /// <summary>
    /// Метод создания экземпляра StructuralElementRepository
    /// </summary>
    public IStructuralElementRepository CreateStructuralElementRepository();
    
    /// <summary>
    /// Метод создания экземпляра MagnetogramRepository
    /// </summary>
    public IMagnetogramRepository CreateMagnetogramRepository();

    /// <summary>
    /// Метод создания экземпляра ReportRepository
    /// </summary>
    public IReportRepository CreateReportRepository();
}