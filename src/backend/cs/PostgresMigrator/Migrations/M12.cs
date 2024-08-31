using FluentMigrator;
using PostgresMigrator.Consts;

namespace PostgresMigrator.Migrations
{
    /// <summary>
    /// Миграция для создания таблицы DEFECT_COMMIT
    /// </summary>
    [Migration(12, "Создание таблицы DEFECT_COMMIT")]
    public class M12 : Migration
    {
        private static readonly string _tableName = "defect_commit";
        private static readonly string _commitTableName = "commit";
        private static readonly string _defectTableName = "defect";
        
        public override void Up()
        {
            // Проверка на существование таблицы перед созданием
            if (!Schema.Schema(Const.Schema).Table(_tableName).Exists())
            {
                Create.Table(_tableName).InSchema(Const.Schema)
                    .WithColumn("id").AsString().PrimaryKey()
                    .WithColumnDescription("Идентификатор связи")

                    .WithColumn("commitid").AsString().NotNullable()
                    .ForeignKey("commitid", Const.Schema, _commitTableName, "id")
                    .WithColumnDescription("Идентификатор данных о магнитограмме")

                    .WithColumn("defectid").AsString().NotNullable()
                    .ForeignKey("defectid", Const.Schema, _defectTableName, "id")
                    .WithColumnDescription("Идентификатор дефекта");
            }
        }

        public override void Down()
        {
            // Проверка на существование таблицы перед удалением
            if (Schema.Schema(Const.Schema).Table(_tableName).Exists())
            {
                Delete.Table(_tableName).InSchema(Const.Schema);
            }
        }
    }
}
