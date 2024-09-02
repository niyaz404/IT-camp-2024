using FluentMigrator;
using PostgresMigrator.Consts;

namespace PostgresMigrator.Migrations
{
    /// <summary>
    /// Миграция для создания таблицы STRUCTURALELEMENT_COMMIT
    /// </summary>
    [Migration(11, "Создание таблицы STRUCTURALELEMENT_COMMIT")]
    public class M11 : Migration
    {
        private static readonly string _tableName = "structuralelement_commit";
        private static readonly string _commitTableName = "commit";
        private static readonly string _structuralelementTableName = "structuralelement";
        
        public override void Up()
        {
            // Проверка на существование таблицы перед созданием
            if (!Schema.Schema(Const.Schema).Table(_tableName).Exists())
            {
                Create.Table(_tableName).InSchema(Const.Schema)
                    .WithColumn("id").AsGuid().PrimaryKey()
                    .WithColumnDescription("Идентификатор связи")

                    .WithColumn("commitid").AsGuid().NotNullable()
                    .ForeignKey("commitid", Const.Schema, _commitTableName, "id")
                    .WithColumnDescription("Идентификатор данных о магнитограмме")

                    .WithColumn("structuralelementid").AsGuid().NotNullable()
                    .ForeignKey("structuralelementid", Const.Schema, _structuralelementTableName, "id")
                    .WithColumnDescription("Идентификатор структурного элемента");
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
