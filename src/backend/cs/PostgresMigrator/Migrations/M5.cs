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
        
        public override void Up()
        {
            // Проверка на существование таблицы перед созданием
            if (!Schema.Schema(Const.Schema).Table(_tableName).Exists())
            {
                Create.Table(_tableName).InSchema(Const.Schema)
                    .WithColumn("id").AsString().PrimaryKey()
                    .WithColumnDescription("Идентификатор элемента")

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
