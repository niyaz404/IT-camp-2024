using Consts;
using FluentMigrator;

namespace PostgresMigrator.Migrations
{
    /// <summary>
    /// Миграция для создания таблицы DEFECT_COMMIT
    /// </summary>
    [Migration(12, $"Создание таблицы {PgTables.DefectToCommit}")]
    public class M12 : Migration
    {
        private static readonly string _tableName = PgTables.DefectToCommit;
        private static readonly string _commitTableName = PgTables.Commit;
        private static readonly string _defectTableName = PgTables.Defect;
        
        public override void Up()
        {
            // Проверка на существование таблицы перед созданием
            if (!Schema.Schema(PgTables.Schema).Table(_tableName).Exists())
            {
                Create.Table(_tableName).InSchema(PgTables.Schema)
                    .WithColumn("id").AsGuid().PrimaryKey().WithDefault(SystemMethods.NewGuid)
                    .WithColumnDescription("Идентификатор связи")

                    .WithColumn("commitid").AsGuid().NotNullable()
                    .ForeignKey("commitid", PgTables.Schema, _commitTableName, "id")
                    .WithColumnDescription("Идентификатор данных о магнитограмме")

                    .WithColumn("defectid").AsGuid().NotNullable()
                    .ForeignKey("defectid", PgTables.Schema, _defectTableName, "id")
                    .WithColumnDescription("Идентификатор дефекта");
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
