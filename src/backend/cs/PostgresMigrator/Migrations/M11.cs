using FluentMigrator;
using PostgresMigrator.Consts;

namespace PostgresMigrator.Migrations
{
    /// <summary>
    /// Миграция для создания таблицы STRUCTURALELEMENT_COMMIT
    /// </summary>
    [Migration(11, "Создание таблицы STRUCTURAL_ELEMENT_COMMIT")]
    public class M11 : Migration
    {
        private static readonly string _tableName = "structural_element_commit";
        private static readonly string _commitTableName = "commit";
        private static readonly string _structuralelementTableName = "structural_element";
        
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

                    .WithColumn("structuralelementid").AsString().NotNullable()
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
