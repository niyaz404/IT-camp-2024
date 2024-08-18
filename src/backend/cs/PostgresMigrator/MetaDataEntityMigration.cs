using FluentMigrator;

namespace PostgresMigrator.Migrations
{
    [Migration(4)]
    public class M4 : Migration
    {
        public override void Up()
        {
            // Проверка на существование таблицы перед созданием
            if (!Schema.Table("MetaData").Exists())
            {
                Create.Table("MetaData")
                    .WithColumn("id").AsString().PrimaryKey()
                    .AddDescription("id", "Идентификатор данных о магнитограмме")

                    .WithColumn("magnetogramid").AsString().NotNullable()
                    .AddDescription("magnetogramid", "Идентификатор магнитограммы")

                    .WithColumn("createdat").AsDateTime().NotNullable()
                    .AddDescription("createdat", "Дата обработки магнитограммы")

                    .WithColumn("createdby").AsString(255).NotNullable()
                    .AddDescription("createdby", "ФИО пользователя, запустившего обработку магнитограммы")

                    .WithColumn("defectids").AsCustom("text[]").Nullable()
                    .AddDescription("defectids", "Список идентификаторов дефектов")

                    .WithColumn("structuralelementids").AsCustom("text[]").Nullable()
                    .AddDescription("structuralelementids", "Список идентификаторов конструктивных элементов")

                    .WithColumn("originalmagnetogram").AsBinary().Nullable()
                    .AddDescription("originalmagnetogram", "Файл исходной магнитограммы")

                    .WithColumn("processedmagnetogram").AsBinary().Nullable()
                    .AddDescription("processedmagnetogram", "Файл обработанной магнитограммы");
            }
        }

        public override void Down()
        {
            // Проверка на существование таблицы перед удалением
            if (Schema.Table("MetaData").Exists())
            {
                Delete.Table("MetaData");
            }
        }
    }
}
