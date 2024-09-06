using Consts;
using FluentMigrator;

namespace PostgresMigrator.Migrations
{
    /// <summary>
    /// Миграция для создания таблицы STRUCTURALELEMENT_COMMIT
    /// </summary>
    [Migration(11, $"Создание таблицы {PgTables.StructuralElementToCommit}")]
    public class M11 : Migration
    {
        private static readonly string _tableName = PgTables.StructuralElementToCommit;
        private static readonly string _commitTableName = PgTables.Commit;
        private static readonly string _structuralelementTableName = PgTables.StructuralElement;
        
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

                    .WithColumn("structuralelementid").AsGuid().NotNullable()
                    .ForeignKey("structuralelementid", PgTables.Schema, _structuralelementTableName, "id")
                    .WithColumnDescription("Идентификатор структурного элемента");
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
