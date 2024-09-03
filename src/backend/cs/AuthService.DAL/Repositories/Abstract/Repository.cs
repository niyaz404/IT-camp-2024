namespace AuthService.DAL.Repositories.Abstract;

/// <summary>
/// Абстрактная сущность репозитория
/// </summary>
public abstract class Repository(string connectionString, string mainTableName)
{
    /// <summary>
    /// Строка подключения к БД
    /// </summary>
    protected readonly string _connectionString = connectionString;

    /// <summary>
    /// Название основной таблицы репозитория
    /// </summary>
    protected readonly string _mainTableName = mainTableName;
}