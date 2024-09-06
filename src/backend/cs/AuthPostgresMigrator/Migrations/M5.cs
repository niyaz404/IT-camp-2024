using AuthPostgresMigrator.Consts;
using FluentMigrator;

namespace AuthPostgresMigrator.Migrations
{
    /// <summary>
    /// Миграция для создания таблицы USER
    /// </summary>
    [Migration(5, "Создание таблицы USER")]
    public class M5 : Migration
    {
        private static readonly string _tableName = "user";
        
        public override void Up()
        {
            // Проверка на существование таблицы перед созданием
            if (!Schema.Schema(Const.Schema).Table(_tableName).Exists())
            {
                Create.Table(_tableName).InSchema(Const.Schema)
                    .WithColumn("id").AsGuid().PrimaryKey().WithDefault(SystemMethods.NewGuid)
                    .WithColumnDescription("Идентификатор пользователя")

                    .WithColumn("username").AsString(255).NotNullable()
                    .WithColumnDescription("ФИО пользователя")

                    .WithColumn("login").AsString(255).NotNullable()
                    .WithColumnDescription("Логин пользователя")

                    .WithColumn("hash").AsString(255).NotNullable()
                    .WithColumnDescription("Хеш пароля")

                    .WithColumn("salt").AsString(255).NotNullable()
                    .WithColumnDescription("Соль");
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
