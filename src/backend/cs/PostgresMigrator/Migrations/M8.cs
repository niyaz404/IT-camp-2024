using Consts;
using FluentMigrator;

namespace PostgresMigrator.Migrations
{
    /// <summary>
    /// Миграция для создания таблицы COMMIT
    /// </summary>
    [Migration(8, $"Создание таблицы {PgTables.Commit}")]
    public class M8 : Migration
    {
        private static readonly string _tableName = PgTables.Commit;
        private static readonly string _magnetogramTableName = PgTables.Magnetogram;
        
        public override void Up()
        {
            // Проверка на существование таблицы перед созданием
            if (!Schema.Schema(PgTables.Schema).Table(_tableName).Exists())
            {
                Create.Table(_tableName).InSchema(PgTables.Schema)
                    .WithColumn("id").AsGuid().PrimaryKey().WithDefault(SystemMethods.NewGuid)
                    .WithColumnDescription("Идентификатор данных о магнитограмме")

                    .WithColumn("magnetogramid").AsGuid().NotNullable()
                    .ForeignKey("magnetogramid", PgTables.Schema, _magnetogramTableName, "id")
                    .WithColumnDescription("Идентификатор магнитограммы")

                    .WithColumn("name").AsString().Nullable()
                    .WithColumnDescription("Название обработки")

                    .WithColumn("createdat").AsDateTime().NotNullable()
                    .WithColumnDescription("Дата обработки магнитограммы")

                    .WithColumn("createdby").AsString(255).NotNullable()
                    .WithColumnDescription("ФИО пользователя, запустившего обработку магнитограммы")

                    .WithColumn("processedimage").AsBinary().Nullable()
                    .WithColumnDescription("Файл обработанной магнитограммы")

                    .WithColumn("originalimage").AsBinary().Nullable()
                    .WithColumnDescription("Файл исходной магнитограммы");
            }
        }

        public override void Down()
        {
            // Проверка на существование таблицы перед удалением
            if (Schema.Schema(PgTables.Schema).Table(_tableName).Exists())
            {
                Delete.Table(_tableName).InSchema(PgTables.Schema);
            }
        }
    }
}
