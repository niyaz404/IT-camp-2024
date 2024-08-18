using FluentMigrator;

namespace PostgresMigrator.Migrations
{
    [Migration(1)]
    public class M1 : Migration
    {
        public override void Up()
        {
            // Проверка на существование таблицы перед созданием
            if (!Schema.Table("Magnetogram").Exists())
            {
                Create.Table("Magnetogram")
                    .WithColumn("id").AsString().PrimaryKey()
                    .AddDescription("id", "Идентификатор магнитограммы")

                    .WithColumn("name").AsString(255).NotNullable()
                    .AddDescription("name", "Название магнитограммы")

                    .WithColumn("objectname").AsString(255).NotNullable()
                    .AddDescription("objectname", "Название объекта")

                    .WithColumn("createdby").AsString(255).NotNullable()
                    .AddDescription("createdby", "ФИО загрузившего магнитограмму")

                    .WithColumn("createdat").AsDateTime().NotNullable()
                    .AddDescription("createdat", "Дата загрузки файла магнитограммы")

                    .WithColumn("file").AsBinary().NotNullable()
                    .AddDescription("file", "Файл магнитограммы в двоичном формате");
            }
        }

        public override void Down()
        {
            // Проверка на существование таблицы перед удалением
            if (Schema.Table("Magnetogram").Exists())
            {
                Delete.Table("Magnetogram");
            }
        }
    }
}
