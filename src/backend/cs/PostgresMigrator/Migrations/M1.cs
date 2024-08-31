using FluentMigrator;
using PostgresMigrator.Consts;

namespace PostgresMigrator.Migrations
{
    /// <summary>
    /// Миграция для создания базовой схемы
    /// </summary>
    [Migration(1, "Создания базовой схемы")]
    public class M1 : Migration
    {
        public override void Up()
        {
            // Проверка на существование схемы
            if (!Schema.Schema(Const.Schema).Exists())
            {
                Create.Schema(Const.Schema);
            }
        }

        public override void Down()
        {
            // Схему не удаляем
        }
    }
}
