using FluentMigrator;
using PostgresMigrator.Consts;

namespace PostgresMigrator.Migrations
{
    /// <summary>
    /// Миграция для заполнения справочника ELEMENT_TYPE
    /// </summary>
    [Migration(4, "Добавление данных в таблицу ELEMENT_TYPE")]
    public class M4 : Migration
    {
        private static readonly string _tableName = "element_type";
        
        public override void Up()
        {
            // Проверка на существование таблицы перед добавлением записей
            if (Schema.Schema(Const.Schema).Table(_tableName).Exists())
            {
                Insert.IntoTable(_tableName).InSchema(Const.Schema)
                    .Row(new { id = 1, name = "Дефект" })
                    .Row(new { id = 2, name = "Конструктивный элемент" });
            }
        }

        public override void Down()
        {
            // Проверка на существование таблицы перед удалением записей
            if (Schema.Schema(Const.Schema).Table(_tableName).Exists())
            {
                Delete.FromTable(_tableName)
                    .Row(new { id = 1, name = "Дефект" })
                    .Row(new { id = 2, name = "Конструктивный элемент" });
            }
        }
    }
}
