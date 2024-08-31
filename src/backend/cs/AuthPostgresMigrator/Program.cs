using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace AuthPostgresMigrator;

class Program
{
    public static void Main(string[] args)
    {
        var services = new ServiceCollection()
            .AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddPostgres()
                .WithGlobalConnectionString(GetConnectionString())
                .ScanIn(typeof(Program).Assembly)
                .For.All())
            .BuildServiceProvider();

        // Применить миграции
        using var scope = services.CreateScope();
        var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
        runner.MigrateUp();
    }

    private static string GetConnectionString()
    {
        // Получить строку подключения из переменных среды
        var connectionString = new Npgsql.NpgsqlConnectionStringBuilder
        {
            Host = Environment.GetEnvironmentVariable("POSTGRES_HOST"),
            Port = int.Parse(Environment.GetEnvironmentVariable("POSTGRES_PORT") ?? ""),
            Username = Environment.GetEnvironmentVariable("POSTGRES_USER"),
            Password = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD"),
            Database = Environment.GetEnvironmentVariable("POSTGRES_DB")
        };

        return connectionString.ToString();
    }
}