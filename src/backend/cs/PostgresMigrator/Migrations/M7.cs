using FluentMigrator;
using PostgresMigrator.Consts;

namespace PostgresMigrator.Migrations
{
    /// <summary>
    /// Миграция для заполнения справочника STRUCTURALELEMENTTYPE
    /// </summary>
    [Migration(7, "Добавление данных в таблицу STRUCTURAL_ELEMENT_TYPE")]
    public class M7 : Migration
    {
        private static readonly string _tableName = "structuralelementtype";
        
        public override void Up()
        {
            // Проверка на существование таблицы перед добавлением записей
            if (Schema.Schema(Const.Schema).Table(_tableName).Exists())
            {
                Insert.IntoTable(_tableName).InSchema(Const.Schema)
                    .Row(new { id = 1, name = "Сварной шов" })
                    .Row(new { id = 2, name = "Изгиб" })
                    .Row(new { id = 3, name = "Разветвление" })
                    .Row(new { id = 4, name = "Заплатка" });
            }
        }

        public override void Down()
        {
            // Проверка на существование таблицы перед удалением записей
            if (Schema.Schema(Const.Schema).Table(_tableName).Exists())
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
