using FluentMigrator;

namespace PostgresMigrator.Migrations
{
    [Migration(3)]
    public class M3 : Migration
    {
        public override void Up()
        {
            // Проверка на существование таблицы перед созданием
            if (!Schema.Table("ElementType").Exists())
            {
                Create.Table("ElementType")
                    .WithColumn("id").AsInt32().PrimaryKey().Identity()
                    .AddDescription("id", "Идентификатор типа")

                    .WithColumn("name").AsString(255).NotNullable()
                    .AddDescription("name", "Наименование типа");
            }
        }

        public override void Down()
        {
            // Проверка на существование таблицы перед удалением
            if (Schema.Table("ElementType").Exists())
            {
                Delete.Table("ElementType");
            }
        }
    }
}
