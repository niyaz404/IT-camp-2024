using AuthPostgresMigrator.Consts;
using FluentMigrator;

namespace AuthPostgresMigrator.Migrations
{
    /// <summary>
    /// Миграция для добавления записей в таблицу ROLE
    /// </summary>
    [Migration(3, "Добавление записей в таблицу ROLE")]
    public class M3 : Migration
    {
        private static readonly string _tableName = "role";
        
        public override void Up()
        {
            // Проверка на существование таблицы перед добавлением записей
            if (Schema.Schema(Const.Schema).Table(_tableName).Exists())
            {
                Insert.IntoTable(_tableName).InSchema(Const.Schema)
                    .Row(new { id = 1, name = "ADMIN" })
                    .Row(new { id = 2, name = "EXPERT" })
                    .Row(new { id = 3, name = "OPERATOR" });
            }
        }

        public override void Down()
        {
            // Проверка на существование таблицы перед удалением записей
            if (Schema.Schema(Const.Schema).Table(_tableName).Exists())
            {
                Delete.FromTable(_tableName)
                    .Row(new { id = 1, name = "ADMIN" })
                    .Row(new { id = 2, name = "EXPERT" })
                    .Row(new { id = 3, name = "OPERATOR" });
            }
        }
    }
}
