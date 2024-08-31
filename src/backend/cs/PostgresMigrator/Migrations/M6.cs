using FluentMigrator;
using PostgresMigrator.Consts;

namespace PostgresMigrator.Migrations
{
    /// <summary>
    /// Миграция для создания таблицы STRUCTURALELEMENTTYPE
    /// </summary>
    [Migration(6, "Создание таблицы STRUCTURAL_ELEMENT_TYPE")]
    public class M6 : Migration
    {
        private static readonly string _tableName = "structural_element_type";
        
        public override void Up()
        {
            // Проверка на существование таблицы перед созданием
            if (!Schema.Schema(Const.Schema).Table(_tableName).Exists())
            {
                Create.Table(_tableName).InSchema(Const.Schema)
                    .WithColumn("id").AsInt32().PrimaryKey().Identity()
                    .WithColumnDescription("Идентификатор типа")

                    .WithColumn("name").AsString(255).NotNullable()
                    .WithColumnDescription("Наименование типа структурного элемента");
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
