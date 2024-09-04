using Consts;
using FluentMigrator;

namespace PostgresMigrator.Migrations
{
    /// <summary>
    /// Миграция для заполнения справочника ELEMENT_TYPE
    /// </summary>
    [Migration(9, $"Добавление данных в таблицу {PgTables.ElementType}")]
    public class M9 : Migration
    {
        private static readonly string _tableName = PgTables.ElementType;
        
        public override void Up()
        {
            // Проверка на существование таблицы перед добавлением записей
            if (Schema.Schema(PgTables.Schema).Table(_tableName).Exists())
            {
                Insert.IntoTable(_tableName).InSchema(PgTables.Schema)
                    .Row(new { id = 1, name = "Дефект" })
                    .Row(new { id = 2, name = "Конструктивный элемент" });
            }
        }

        public override void Down()
        {
            // Проверка на существование таблицы перед удалением записей
            if (Schema.Schema(PgTables.Schema).Table(_tableName).Exists())
            {
                Delete.FromTable(_tableName)
                    .Row(new { id = 1, name = "Дефект" })
                    .Row(new { id = 2, name = "Конструктивный элемент" });
            }
        }
    }
}
