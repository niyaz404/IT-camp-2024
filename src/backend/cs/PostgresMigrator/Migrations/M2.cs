using Consts;
using FluentMigrator;

namespace PostgresMigrator.Migrations
{
    /// <summary>
    /// Миграция для создания таблицы MAGNETOGRAM
    /// </summary>
    [Migration(2, $"Создание таблицы {PgTables.Magnetogram}")]
    public class M2 : Migration
    {
        private static readonly string _tableName = PgTables.Magnetogram;
        
        public override void Up()
        {
            Execute.Sql("create extension if not exists \"uuid-ossp\";");
            
            // Проверка на существование таблицы перед созданием
            if (!Schema.Schema(PgTables.Schema).Table(_tableName).Exists())
            {
                Create.Table(_tableName).InSchema(PgTables.Schema)
                    .WithColumn("id").AsGuid().PrimaryKey().WithDefault(SystemMethods.NewGuid)
                    .WithColumnDescription("Идентификатор магнитограммы")

                    .WithColumn("createdby").AsString(255).NotNullable()
                    .WithColumnDescription("ФИО загрузившего магнитограмму")

                    .WithColumn("createdat").AsDateTime().NotNullable()
                    .WithColumnDescription("Дата загрузки файла магнитограммы")

                    .WithColumn("file").AsBinary().NotNullable()
                    .WithColumnDescription("Файл магнитограммы в двоичном формате");
            }
        }

        public override void Down()
        {
            // Проверка на существование таблицы перед удалением
            if (Schema.Schema(PgTables.Schema).Table(_tableName).Exists())
            {
                Delete.Table(_tableName).InSchema(PgTables.Schema);
            }
        }
    }
}
