using Consts;
using FluentMigrator;

namespace PostgresMigrator.Migrations
{
    /// <summary>
    /// Миграция для заполнения справочника STRUCTURALELEMENTTYPE
    /// </summary>
    [Migration(7, $"Добавление данных в таблицу {PgTables.StructuralElementType}")]
    public class M7 : Migration
    {
        private static readonly string _tableName = PgTables.StructuralElementType;
        
        public override void Up()
        {
            // Проверка на существование таблицы перед добавлением записей
            if (Schema.Schema(PgTables.Schema).Table(_tableName).Exists())
            {
                Insert.IntoTable(_tableName).InSchema(PgTables.Schema)
                    .Row(new { id = 1, name = "Сварной шов" })
                    .Row(new { id = 2, name = "Изгиб" })
                    .Row(new { id = 3, name = "Разветвление" })
                    .Row(new { id = 4, name = "Заплатка" });
            }
        }

        public override void Down()
        {
            // Проверка на существование таблицы перед удалением записей
            if (Schema.Schema(PgTables.Schema).Table(_tableName).Exists())
            {
                Delete.FromTable(_tableName)
                    .Row(new { id = 1, name = "Сварной шов" })
                    .Row(new { id = 2, name = "Изгиб" })
                    .Row(new { id = 3, name = "Разветвление" })
                    .Row(new { id = 4, name = "Заплатка" });
            }
        }
    }
}
