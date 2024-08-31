using FluentMigrator;
using PostgresMigrator.Consts;

namespace PostgresMigrator.Migrations
{
    /// <summary>
    /// Миграция для создания таблицы COMMIT
    /// </summary>
    [Migration(8, "Создание таблицы COMMIT")]
    public class M8 : Migration
    {
        private static readonly string _tableName = "commit";
        private static readonly string _magnetogramTableName = "magnetogram";
        
        public override void Up()
        {
            // Проверка на существование таблицы перед созданием
            if (!Schema.Schema(Const.Schema).Table(_tableName).Exists())
            {
                Create.Table(_tableName).InSchema(Const.Schema)
                    .WithColumn("id").AsString().PrimaryKey()
                    .WithColumnDescription("Идентификатор данных о магнитограмме")

                    .WithColumn("magnetogramid").AsString().NotNullable()
                    .ForeignKey("magnetogramid", Const.Schema, _magnetogramTableName, "id")
                    .WithColumnDescription("Идентификатор магнитограммы")

                    .WithColumn("createdat").AsDateTime().NotNullable()
                    .WithColumnDescription("Дата обработки магнитограммы")

                    .WithColumn("createdby").AsString(255).NotNullable()
                    .WithColumnDescription("ФИО пользователя, запустившего обработку магнитограммы")

                    .WithColumn("processedmagnetogram").AsBinary().Nullable()
                    .WithColumnDescription("Файл обработанной магнитограммы");
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
