using Consts;
using FluentMigrator;

namespace PostgresMigrator.Migrations
{
    /// <summary>
    /// Миграция для создания таблицы REPORT
    /// </summary>
    [Migration(10, $"Создание таблицы {PgTables.Report}")]
    public class M10 : Migration
    {
        private static readonly string _tableName = PgTables.Report;
        private static readonly string _commitTableName = PgTables.Commit;
        
        public override void Up()
        {
            // Проверка на существование таблицы перед созданием
            if (!Schema.Schema(PgTables.Schema).Table(_tableName).Exists())
            {
                Create.Table(_tableName).InSchema(PgTables.Schema)
                    .WithColumn("id").AsGuid().PrimaryKey().WithDefault(SystemMethods.NewGuid)
                    .WithColumnDescription("Идентификатор отчета")

                    .WithColumn("commitid").AsGuid().NotNullable()
                    .ForeignKey("commitid", PgTables.Schema, _commitTableName, "id")
                    .WithColumnDescription("Идентификатор данных о магнитограмме")

                    .WithColumn("createdat").AsDateTime().NotNullable()
                    .WithColumnDescription("Дата создания отчета")

                    .WithColumn("file").AsBinary().NotNullable()
                    .WithColumnDescription("Файл отчета"); //TODO: нужно ли???????
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
