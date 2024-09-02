using FluentMigrator;
using PostgresMigrator.Consts;

namespace PostgresMigrator.Migrations
{
    /// <summary>
    /// Миграция для создания таблицы DEFECT
    /// </summary>
    [Migration(5, "Создание таблицы DEFECT")]
    public class M5 : Migration
    {
        private static readonly string _tableName = "defect";
        private static readonly string _structuralElementTableName = "structuralelement";
        
        public override void Up()
        {
            // Проверка на существование таблицы перед созданием
            if (!Schema.Schema(Const.Schema).Table(_tableName).Exists())
            {
                Create.Table(_tableName).InSchema(Const.Schema)
                    .WithColumn("id").AsGuid().PrimaryKey()
                    .WithColumnDescription("Идентификатор элемента")

                    .WithColumn("description").AsString(1000).Nullable()
                    .WithColumnDescription("Дополнительное описание элемента")

                    .WithColumn("color").AsString(30).Nullable()
                    .WithColumnDescription("Цвет метки на магнитограмме")

                    .WithColumn("startx").AsInt32().NotNullable()
                    .WithColumnDescription("Координата X начала области")

                    .WithColumn("endx").AsInt32().NotNullable()
                    .WithColumnDescription("Координата X конца области")

                    .WithColumn("leftstructuralelementid").AsGuid().NotNullable()
                    .ForeignKey("leftstructuralelementid", Const.Schema, _structuralElementTableName, "id")
                    .WithColumnDescription("Идентификатор структурного элемента слева")

                    .WithColumn("rightstructuralelementid").AsGuid().NotNullable()
                    .ForeignKey("rightstructuralelementid", Const.Schema, _structuralElementTableName, "id")
                    .WithColumnDescription("Идентификатор структурного элемента справа");
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
