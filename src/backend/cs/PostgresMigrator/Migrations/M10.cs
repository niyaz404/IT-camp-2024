using FluentMigrator;
using PostgresMigrator.Consts;

namespace PostgresMigrator.Migrations
{
    /// <summary>
    /// Миграция для создания таблицы REPORT
    /// </summary>
    [Migration(10, "Создание таблицы REPORT")]
    public class M10 : Migration
    {
        private static readonly string _tableName = "report";
        private static readonly string _commitTableName = "commit";
        
        public override void Up()
        {
            // Проверка на существование таблицы перед созданием
            if (!Schema.Schema(Const.Schema).Table(_tableName).Exists())
            {
                Create.Table(_tableName).InSchema(Const.Schema)
                    .WithColumn("id").AsString().PrimaryKey()
                    .WithColumnDescription("Идентификатор отчета")

                    .WithColumn("commitid").AsString().NotNullable()
                    .ForeignKey("commitid", Const.Schema, _commitTableName, "id")
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
            if (Schema.Schema(Const.Schema).Table(_tableName).Exists())
            {
                Delete.Table(_tableName).InSchema(Const.Schema);
            }
        }
    }
}
