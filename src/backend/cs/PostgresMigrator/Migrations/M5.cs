using Consts;
using FluentMigrator;

namespace PostgresMigrator.Migrations
{
    /// <summary>
    /// Миграция для создания таблицы DEFECT
    /// </summary>
    [Migration(5, $"Создание таблицы {PgTables.Defect}")]
    public class M5 : Migration
    {
        private static readonly string _tableName = PgTables.Defect;
        
        public override void Up()
        {
            // Проверка на существование таблицы перед созданием
            if (!Schema.Schema(PgTables.Schema).Table(_tableName).Exists())
            {
                Create.Table(_tableName).InSchema(PgTables.Schema)
                    .WithColumn("id").AsGuid().PrimaryKey().WithDefault(SystemMethods.NewGuid)
                    .WithColumnDescription("Идентификатор элемента")

                    .WithColumn("description").AsString(1000).Nullable()
                    .WithColumnDescription("Дополнительное описание элемента")

                    .WithColumn("startx").AsInt32().NotNullable()
                    .WithColumnDescription("Координата X начала области")

                    .WithColumn("endx").AsInt32().NotNullable()
                    .WithColumnDescription("Координата X конца области");
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
