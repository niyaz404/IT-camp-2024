using FluentMigrator;
using PostgresMigrator.Consts;

namespace PostgresMigrator.Migrations
{
    /// <summary>
    /// Миграция для создания таблицы STRUCTURALELEMENT
    /// </summary>
    [Migration(4, "Создание таблицы STRUCTURALELEMENT")]
    public class M9 : Migration
    {
        private static readonly string _tableName = "structuralelement";
        private static readonly string _elementTypeTableName = "structuralelementtype";
        
        public override void Up()
        {
            // Проверка на существование таблицы перед созданием
            if (!Schema.Schema(Const.Schema).Table(_tableName).Exists())
            {
                Create.Table(_tableName).InSchema(Const.Schema)
                    .WithColumn("id").AsGuid().PrimaryKey()
                    .WithColumnDescription("Идентификатор элемента")

                    .WithColumn("color").AsString(30).Nullable()
                    .WithColumnDescription("Цвет метки на магнитограмме")

                    .WithColumn("elementtypeid").AsInt32().NotNullable()
                    .ForeignKey("elementtypeid", Const.Schema, _elementTypeTableName, "id")
                    .WithColumnDescription("Идентификатор типа структурного элемента")

                    .WithColumn("startx").AsInt32().NotNullable()
                    .WithColumnDescription("Координата X начала области")

                    .WithColumn("endx").AsInt32().NotNullable()
                    .WithColumnDescription("Координата X конца области");
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
