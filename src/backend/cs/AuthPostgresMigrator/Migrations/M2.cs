using AuthPostgresMigrator.Consts;
using FluentMigrator;

namespace AuthPostgresMigrator.Migrations
{
    /// <summary>
    /// Миграция для создания таблицы ROLE
    /// </summary>
    [Migration(2, "Создание таблицы ROLE")]
    public class M2 : Migration
    {
        private static readonly string _tableName = "role";
        
        public override void Up()
        {
            // Проверка на существование таблицы перед созданием
            if (!Schema.Schema(Const.Schema).Table(_tableName).Exists())
            {
                Create.Table(_tableName).InSchema(Const.Schema)
                    .WithColumn("id").AsInt32().PrimaryKey()
                    .WithColumnDescription("Идентификатор роли")

                    .WithColumn("name").AsString(255).NotNullable()
                    .WithColumnDescription("Название роли");
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
