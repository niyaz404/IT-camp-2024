using FluentMigrator;
using PostgresMigrator.Consts;

namespace PostgresMigrator.Migrations
{
    /// <summary>
    /// Миграция для создания таблицы STRUCTURALELEMENT
    /// </summary>
    [Migration(9, "Создание таблицы STRUCTURAL_ELEMENT")]
    public class M9 : Migration
    {
        private static readonly string _tableName = "structural_element";
        private static readonly string _elementTypeTableName = "structural_element_type";
        
        public override void Up()
        {
            // Проверка на существование таблицы перед созданием
            if (!Schema.Schema(Const.Schema).Table(_tableName).Exists())
            {
                Create.Table(_tableName).InSchema(Const.Schema)
                    .WithColumn("id").AsString().PrimaryKey()
                    .WithColumnDescription("Идентификатор элемента")

                    .WithColumn("typeid").AsInt32().NotNullable()
                    .ForeignKey("typeid", Const.Schema, _elementTypeTableName, "id")
                    .WithColumnDescription("Тип элемента на магнитограмме")

                    .WithColumn("description").AsString(1000).Nullable()
                    .WithColumnDescription("Дополнительное описание элемента")

                    .WithColumn("x").AsInt32().NotNullable()
                    .WithColumnDescription("Координата X на магнитограмме");
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
