using AuthPostgresMigrator.Consts;
using FluentMigrator;

namespace AuthPostgresMigrator.Migrations
{
    /// <summary>
    /// Миграция для создания таблицы USER_ROLE
    /// </summary>
    [Migration(6, "Создание таблицы USER_ROLE")]
    public class M6 : Migration
    {
        private static readonly string _tableName = "user_role";
        private static readonly string _userTableName = "user";
        private static readonly string _roleTableName = "role";
        
        public override void Up()
        {
            // Проверка на существование таблицы перед созданием
            if (!Schema.Schema(Const.Schema).Table(_tableName).Exists())
            {
                Create.Table(_tableName).InSchema(Const.Schema)
                    .WithColumn("id").AsGuid().PrimaryKey().WithDefault(SystemMethods.NewGuid)
                    .WithColumnDescription("Идентификатор связи")

                    .WithColumn("userid").AsGuid().NotNullable()
                    .ForeignKey("userid", Const.Schema, _userTableName, "id")
                    .WithColumnDescription("Идентификатор пользователя")

                    .WithColumn("roleid").AsInt32().NotNullable()
                    .ForeignKey("roleid", Const.Schema, _roleTableName, "id")
                    .WithColumnDescription("Идентификатор роли");
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
