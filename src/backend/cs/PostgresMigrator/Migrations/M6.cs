using Consts;
using FluentMigrator;

namespace PostgresMigrator.Migrations
{
    /// <summary>
    /// Миграция для создания таблицы ELEMENTTYPE
    /// </summary>
    [Migration(6, $"Создание таблицы {PgTables.ElementType}")]
    public class M6 : Migration
    {
        private static readonly string _tableName = PgTables.ElementType;
        
        public override void Up()
        {
            // Проверка на существование таблицы перед созданием
            if (!Schema.Schema(PgTables.Schema).Table(_tableName).Exists())
            {
                Create.Table(_tableName).InSchema(PgTables.Schema)
                    .WithColumn("id").AsInt32().PrimaryKey().Identity()
                    .WithColumnDescription("Идентификатор типа")

                    .WithColumn("name").AsString(255).NotNullable()
                    .WithColumnDescription("Наименование типа");
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
