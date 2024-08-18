using FluentMigrator;

namespace PostgresMigrator.Migrations
{
    [Migration(6)]
    public class M6 : Migration
    {
        public override void Up()
        {
            // Проверка на существование таблицы перед созданием
            if (!Schema.Table("Report").Exists())
            {
                Create.Table("Report")
                    .WithColumn("id").AsString().PrimaryKey()
                    .AddDescription("id", "Идентификатор отчета")

                    .WithColumn("metadataid").AsString().NotNullable()
                    .AddDescription("metadataid", "Идентификатор данных о магнитограмме")

                    .WithColumn("createdat").AsDateTime().NotNullable()
                    .AddDescription("createdat", "Дата создания отчета")

                    .WithColumn("file").AsBinary().NotNullable()
                    .AddDescription("file", "Файл отчета");
            }
        }

        public override void Down()
        {
            // Проверка на существование таблицы перед удалением
            if (Schema.Table("Report").Exists())
            {
                Delete.Table("Report");
            }
        }
    }
}
