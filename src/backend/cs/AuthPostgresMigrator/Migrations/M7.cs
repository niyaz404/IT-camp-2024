using AuthPostgresMigrator.Consts;
using FluentMigrator;

namespace AuthPostgresMigrator.Migrations
{
    /// <summary>
    /// Добавление тестовый данных в таблицу USER
    /// </summary>
    [Migration(10, "Добавление тестовый данных в таблицу USER")]
    public class M10 : Migration
    {
        private static readonly string _tableName = "user";
        
        public override void Up()
        {
            // Проверка на существование таблицы перед добавлением записей
            if (Schema.Schema(Const.Schema).Table(_tableName).Exists())
            {
                Insert.IntoTable(_tableName).InSchema(Const.Schema)
                    .Row(new
                    {
                        username = "Галиев Нияз Рафисович", login = "niyaz",
                        hash = "P1gQM+EWP1pq8E6IlqLrMZO2ASHVBNghLo0Klbasrx0=", salt = "YkZ7CHedoBlYCLO6RdqNVQ=="
                    });
            }
        }

        public override void Down()
        {
            // Проверка на существование таблицы перед удалением записей
            if (Schema.Schema(Const.Schema).Table(_tableName).Exists())
            {
                Delete.FromTable(_tableName)
                    .Row(new
                    {
                        username = "Галиев Нияз Рафисович", login = "niyaz",
                        hash = "P1gQM+EWP1pq8E6IlqLrMZO2ASHVBNghLo0Klbasrx0=", salt = "YkZ7CHedoBlYCLO6RdqNVQ=="
                    });
            }
        }
    }
}
