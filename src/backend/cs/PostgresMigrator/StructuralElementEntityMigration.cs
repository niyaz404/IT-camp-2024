using FluentMigrator;

namespace PostgresMigrator.Migrations
{
    [Migration(5)]
    public class M5 : Migration
    {
        public override void Up()
        {
            // Проверка на существование таблицы перед созданием
            if (!Schema.Table("StructuralElement").Exists())
            {
                Create.Table("StructuralElement")
                    .WithColumn("id").AsString().PrimaryKey()
                    .AddDescription("id", "Id элемента")

                    .WithColumn("type").AsInt32().NotNullable()
                    .AddDescription("type", "Тип элемента на магнитограмме")

                    .WithColumn("description").AsString(1000).Nullable()
                    .AddDescription("description", "Дополнительное описание элемента")

                    .WithColumn("x").AsInt32().NotNullable()
                    .AddDescription("x", "Координата X на магнитограмме")

                    .WithColumn("y").AsInt32().NotNullable()
                    .AddDescription("y", "Координата Y на магнитограмме")

                    .WithColumn("leftneighbourid").AsString().Nullable()
                    .AddDescription("leftneighbourid", "Соседний элемент слева на магнитограмме")

                    .WithColumn("leftneighbourtype").AsInt32().NotNullable()
                    .AddDescription("leftneighbourtype", "Тип левого соседа. 1 - дефект, 2 - конструктивный элемент")

                    .WithColumn("rightneighbourid").AsString().Nullable()
                    .AddDescription("rightneighbourid", "Соседний элемент справа на магнитограмме")

                    .WithColumn("rightneighbourtype").AsInt32().NotNullable()
                    .AddDescription("rightneighbourtype", "Тип правого соседа. 1 - дефект, 2 - конструктивный элемент");
            }
        }

        public override void Down()
        {
            // Проверка на существование таблицы перед удалением
            if (Schema.Table("StructuralElement").Exists())
            {
                Delete.Table("StructuralElement");
            }
        }
    }
}
