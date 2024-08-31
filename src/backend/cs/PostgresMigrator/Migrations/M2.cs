using FluentMigrator;
using PostgresMigrator.Consts;

namespace PostgresMigrator.Migrations
{
    /// <summary>
    /// Миграция для создания таблицы MAGNETOGRAM
    /// </summary>
    [Migration(2, "Создание таблицы MAGNETOGRAM")]
    public class M2 : Migration
    {
        private static readonly string _tableName = "magnetogram";
        
        public override void Up()
        {
            // Проверка на существование таблицы перед созданием
            if (!Schema.Schema(Const.Schema).Table(_tableName).Exists())
            {
                Create.Table(_tableName).InSchema(Const.Schema)
                    .WithColumn("id").AsString().PrimaryKey()
                    .WithColumnDescription("Идентификатор магнитограммы")

                    .WithColumn("name").AsString(255).NotNullable()
                    .WithColumnDescription("Название магнитограммы")

                    .WithColumn("objectname").AsString(255).NotNullable()
                    .WithColumnDescription("Название объекта")

                    .WithColumn("createdby").AsString(255).NotNullable()
                    .WithColumnDescription("ФИО загрузившего магнитограмму")

                    .WithColumn("createdat").AsDateTime().NotNullable()
                    .WithColumnDescription("Дата загрузки файла магнитограммы")

                    .WithColumn("file").AsBinary().NotNullable()
                    .WithColumnDescription("Файл магнитограммы в двоичном формате");
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
