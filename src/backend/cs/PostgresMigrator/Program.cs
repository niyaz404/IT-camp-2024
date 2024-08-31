using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace PostgresMigrator;

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
            Host = Environment.GetEnvironmentVariable("POSTGRES_HOST") ?? "localhost",
            Port = int.Parse(Environment.GetEnvironmentVariable("POSTGRES_PORT") ?? "5432"),
            Username = Environment.GetEnvironmentVariable("POSTGRES_USER") ?? "postgres",
            Password = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD") ?? "password",
            Database = Environment.GetEnvironmentVariable("POSTGRES_DB") ?? "postgres"
        };

        return connectionString.ToString();
    }
}
